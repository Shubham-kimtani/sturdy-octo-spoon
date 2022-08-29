using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Web;
using Glass.Mapper.Sc.Web.Mvc;
using $moduleNamespace$.DependencyInjection.Attributes;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Globalization;
using Sitecore.Publishing;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace $moduleNamespace$.Services
{
    [Service(typeof(IEntityService))]
public class SitecoreEntityService : IEntityService
{
    protected readonly ISitecoreService _service;
    protected readonly IMvcContext _mvcContext;
    protected readonly IRequestContext _requestContext;

    public SitecoreEntityService(ISitecoreService service, IMvcContext mvcContext, IRequestContext requestContext)
    {
        _service = service;
        _mvcContext = mvcContext;
        _requestContext = requestContext;
    }

    public TModel Get<TModel>(Guid id) where TModel : class
    {
        return _service.GetItem<TModel>(new GetItemByIdOptions() { Id = id });
    }

    public TModel Get<TModel>(Guid id, bool useMaster = false) where TModel : class
    {
        if (useMaster)
        {
            var sitecoreService = new SitecoreService("master");
            return sitecoreService.GetItem<TModel>(new GetItemByIdOptions() { Id = id });
        }
        else
        {
            return _service.GetItem<TModel>(new GetItemByIdOptions() { Id = id });
        }
    }

    public TModel Get<TModel>(Guid id, string language = "en") where TModel : class
    {
        return _service.GetItem<TModel>(new GetItemByIdOptions() { Id = id, Language = LanguageManager.GetLanguage(language) });
    }

    public TModel Get<TModel>(string path, string language = "en") where TModel : class
    {
        return _service.GetItem<TModel>(new GetItemByPathOptions() { Path = path, Language = LanguageManager.GetLanguage(language) });
    }

    public TModel Get<TModel>(Guid parentId, string path, string language = "en") where TModel : class
    {
        try
        {
            var parentItem = _service.GetItem<GlassBase>(new GetItemByIdOptions() { Id = parentId });
            return _service.GetItem<TModel>(new GetItemByPathOptions() { Path = parentItem.Path + path });
        }
        catch (Exception ex)
        {
        }
        return null;
    }

    public IEnumerable<TModel> GetAll<TModel>(string query) where TModel : class
    {
        return _service.GetItems<TModel>(new GetItemsByQueryOptions() { Query = Query.New(query) });
    }

    public IEnumerable<TModel> GetAll<TModel>(string query, string language) where TModel : class
    {
        return _service.GetItems<TModel>(new GetItemsByQueryOptions() { Query = Query.New(query), Language = LanguageManager.GetLanguage(language) });
    }

    public IEnumerable<TModel> GetChildren<TModel>(Guid parentId, string path, bool useMaster = false) where TModel : class
    {
        if (useMaster)
        {
            var sitecoreService = new SitecoreService("master");
            var grandParentItem = sitecoreService.GetItem<GlassBase>(new GetItemByIdOptions() { Id = parentId });
            var parentItem = sitecoreService.GetItem<GlassBaseParent>(new GetItemByPathOptions() { Path = grandParentItem.Path + path });

            return sitecoreService.GetItems<TModel>(new GetItemsByQueryOptions() { Query = Query.New(parentItem.Path + "/*") });
        }
        else
        {
            var grandParentItem = _service.GetItem<GlassBase>(new GetItemByIdOptions() { Id = parentId });
            var parentItem = _service.GetItem<GlassBaseParent>(new GetItemByPathOptions() { Path = grandParentItem.Path + path });

            return _service.GetItems<TModel>(new GetItemsByQueryOptions() { Query = Query.New(parentItem.Path + "/*") });
        }
    }

    public TModel GetParentOfType<TModel>(Guid entityId) where TModel : class
    {
        var typeConfiguration = _service.GlassContext.TypeConfigurations[typeof(TModel)] as SitecoreTypeConfiguration;

        if (typeConfiguration == null)
        {
            throw new InvalidOperationException($"Could not get configuration for type {typeof(TModel).Name}");
        }

        var item = _service.Database.GetItem(new ID(entityId));

        if (item == null)
        {
            return null;
        }

        var parentItem = item.Axes.SelectSingleItem($"ancestor-or-self::*[@@templateid='{typeConfiguration.TemplateId}']");

        return _service.GetItem<TModel>(new GetItemByItemOptions(parentItem));
    }

    public IEnumerable<TModel> GetChildrenOfType<TModel>(Guid entityId, Guid templateId, bool allExceptThis = false) where TModel : class
    {
        var item = _service.Database.GetItem(new ID(entityId));
        var childrens = new List<Item>();
        var result = new List<TModel>();

        if (item == null)
            return Enumerable.Empty<TModel>();

        if (allExceptThis)
        {
            childrens = item.GetChildren().Where(a => a.TemplateID.Guid != templateId).ToList();
        }
        else
        {
            childrens = item.GetChildren().Where(a => a.TemplateID.Guid == templateId).ToList();
        }

        foreach (var child in childrens)
        {
            result.Add(_service.GetItem<TModel>(new GetItemByItemOptions(child)));
        }
        var results = result.Where(items => items != null);
        return results;
    }

    public TModel Create<TModel>(Guid parentId, TModel model) where TModel : class
    {
        var parentItem = _service.GetItem<GlassBase>(new GetItemByIdOptions() { Id = parentId });

        using (new SecurityDisabler())
        {
            var item = _service.CreateItem<TModel>(new CreateByModelOptions() { Model = model, Parent = parentItem, Type = model.GetType() });
            return item;
        }
    }

    public TModel Create<TModel>(string parentPath, TModel model) where TModel : class
    {
        var parentItem = _service.GetItem<GlassBase>(new GetItemByPathOptions() { Path = parentPath });

        using (new SecurityDisabler())
        {
            var item = _service.CreateItem<TModel>(new CreateByModelOptions() { Model = model, Parent = parentItem, Type = model.GetType() });
            return item;
        }
    }

    public TModel Create<TModel>(string parentPath, TModel model, string language) where TModel : class
    {
        var parentItem = _service.GetItem<GlassBase>(new GetItemByPathOptions() { Path = parentPath, Language = LanguageManager.GetLanguage(language) });

        using (new SecurityDisabler())
        {
            using (new LanguageSwitcher(language))
            {
                var item = _service.CreateItem<TModel>(new CreateByModelOptions() { Model = model, Parent = parentItem, Type = model.GetType() });
                return item;
            }
        }
    }

    public TModel Create<TModel>(Guid parentId, string relativePath, TModel model) where TModel : class
    {
        var grandParentItem = _service.GetItem<GlassBase>(new GetItemByIdOptions() { Id = parentId });

        using (new SecurityDisabler())
        {
            var parentItem = _service.GetItem<GlassBase>(new GetItemByPathOptions() { Path = grandParentItem.Path + relativePath });
            var item = _service.CreateItem<TModel>(new CreateByModelOptions() { Model = model, Parent = parentItem, Type = model.GetType() });
            return item;
        }
    }

    public bool Update<TModel>(TModel model, bool useMaster = false) where TModel : class
    {
        if (useMaster)
        {
            var sitecoreService = new SitecoreService("master");
            using (new SecurityDisabler())
            {
                sitecoreService.SaveItem(model);
            }
        }
        else
        {
            using (new SecurityDisabler())
            {
                _service.SaveItem(model);
            }
        }
        return true;
    }

    public void Publish(Guid entityId)
    {
        var item = _service.Database.GetItem(new ID(entityId));

        if (item == null)
        {
            throw new InvalidDataException($"Could not find entity with id {entityId}");
        }

        using (new SecurityDisabler())
        {
            Database[] databases = new Database[] { Factory.GetDatabase("web") };
            Sitecore.Globalization.Language[] languages = new Sitecore.Globalization.Language[] { LanguageManager.DefaultLanguage };
            PublishManager.PublishItem(item, databases, languages, true, true);
        }
    }

    public bool Delete(Guid id)
    {
        var item = _service.GetItem<GlassBase>(new GetItemByIdOptions() { Id = id });

        if (item != null)
        {
            using (new SecurityDisabler())
            {
                _service.DeleteItem(new DeleteByModelOptions() { Model = item });
            }
            return true;
        }
        return false;
    }

    public void DeleteItem<TModel>(TModel model) where TModel : class
    {
        using (new SecurityDisabler())
        {
            _service.DeleteItem(model);
        }
    }
}
}