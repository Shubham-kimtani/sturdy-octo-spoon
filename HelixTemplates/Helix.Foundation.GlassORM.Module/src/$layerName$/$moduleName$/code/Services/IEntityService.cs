using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace $moduleNamespace$.Services
{
    public interface IEntityService
    {
        TModel Get<TModel>(Guid id) where TModel : class;

        TModel Get<TModel>(Guid id, bool useMaster = false) where TModel : class;

        TModel Get<TModel>(Guid id, string language = "en") where TModel : class;

        TModel Get<TModel>(string path, string language = "en") where TModel : class;

        TModel Get<TModel>(Guid parentId, string path, string language = "en") where TModel : class;

        IEnumerable<TModel> GetAll<TModel>(string query) where TModel : class;

        IEnumerable<TModel> GetAll<TModel>(string query, string language) where TModel : class;

        IEnumerable<TModel> GetChildren<TModel>(Guid parentId, string path, bool useMaster = false) where TModel : class;

        TModel GetParentOfType<TModel>(Guid entityId) where TModel : class;

        IEnumerable<TModel> GetChildrenOfType<TModel>(Guid entityId, Guid templateId, bool allExceptThis = false) where TModel : class;

        TModel Create<TModel>(Guid parentId, TModel model) where TModel : class;

        TModel Create<TModel>(string parentPath, TModel model) where TModel : class;

        TModel Create<TModel>(string parentPath, TModel model, string language) where TModel : class;

        TModel Create<TModel>(Guid parentId, string relativePath, TModel model) where TModel : class;

        bool Update<TModel>(TModel model, bool useMaster = false) where TModel : class;

        void Publish(Guid entityId);

        bool Delete(Guid id);

        void DeleteItem<TModel>(TModel model) where TModel : class;
    }
}