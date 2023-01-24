using DevExtreme.AspNet.Data.Helpers;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace Ikari.Data.Models.ViewModels.DataGridModels {
    [ModelBinder(BinderType = typeof(DataSourceLoadOptionsBinder))]
    public class DataGridLoadOptions : DataSourceLoadOptionsBase {
    }

    public class DataSourceLoadOptionsBinder : IModelBinder {

        public Task BindModelAsync(ModelBindingContext bindingContext) {
            var loadOptions = new DataGridLoadOptions();
            DataSourceLoadOptionsParser.Parse(loadOptions, key => bindingContext.ValueProvider.GetValue(key).FirstOrDefault());
            bindingContext.Result = ModelBindingResult.Success(loadOptions);
            return Task.CompletedTask;
        }

    }
}
