using Microsoft.Extensions.Options;
using Parbandhan.Interfaces;
using Parbandhan.Models;
using Parbandhan.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Parbandhan.ViewModels
{
    public class AddAssetViewModel : ViewModelBase
    {
        private readonly IAssetTypeService _assetTypeService;
        private readonly IStorageLocationService _storageLocationService;
        private readonly DynamicField _dynamicField;

        public ObservableCollection<AssetTypeModel> AssetTypes { get; set; } = new ObservableCollection<AssetTypeModel>();
        public ObservableCollection<StorageLocationModel> StorageLocations { get; set; } = new ObservableCollection<StorageLocationModel>();
        public ObservableCollection<DynamicField> DynamicFields { get; set; } = new ObservableCollection<DynamicField>();

        private int _selectedAssetTypeId = 0;
        private AssetTypeModel _selectedAssetType;
        private int _selectedStorageLocationId = 0;
        private StorageLocationModel _selectedStorageLocation;

        [Range(1, int.MaxValue, ErrorMessage = "Asset Type is required!")]
        public int SelectedAssetTypeId
        {
            get => _selectedAssetTypeId;
            set
            {
                if (_selectedAssetTypeId == value)
                    return;

                SetProperty(ref _selectedAssetTypeId, value, true);
                OnAssetTypeChanged();
            }
        }

        public AssetTypeModel SelectedAssetType
        {
            get => _selectedAssetType;
            set
            {
                if (_selectedAssetType == value)
                    return;

                _selectedAssetType = value;
                OnPropertyChanged(nameof(SelectedAssetType));

                OnAssetTypeChanged();
                LoadDynamicFields();
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Storage Location is required!")]
        public int SelectedStorageLocationId
        {
            get => _selectedStorageLocationId;
            set => SetProperty(ref _selectedStorageLocationId, value, true);
        }

        public StorageLocationModel SelectedStorageLocation
        {
            get => _selectedStorageLocation;
            set => SetProperty(ref _selectedStorageLocation, value, true);
        }

        public AddAssetViewModel(IAssetTypeService assetTypeService, IStorageLocationService storageLocationService, DynamicField dynamicField)
        {
            _assetTypeService = assetTypeService;
            _storageLocationService = storageLocationService;
            _dynamicField = dynamicField;
            _ = LoadAssetTypesAsync();
        }

        private async Task LoadAssetTypesAsync()
        {
            var response = await _assetTypeService.GetAssetTypesAsync();

            AssetTypes.Clear();
            if (response?.Result?.Data == null) return;

            AssetTypes.Add(new AssetTypeModel { Atid = 0, AtName = "Select Asset Type" });
            foreach (var item in response.Result.Data)
            {
                AssetTypes.Add(item);
            }
        }

        private async void OnAssetTypeChanged()
        {
            if (SelectedAssetTypeId == 0)
                return;

            await LoadStorageLocationByAssetTypeAsync(new[] { SelectedAssetTypeId });
        }

        private async Task LoadStorageLocationByAssetTypeAsync(IEnumerable<int> assetTypeIds)
        {
            var response = await _storageLocationService.GetByAssetTypesAsync(assetTypeIds);

            StorageLocations.Clear();
            if (response?.Result?.Data == null) return;
            StorageLocations.Add(new StorageLocationModel { Slid = 0, SlName = "Select Storage Location" });
            foreach (var item in response.Result.Data)
            {
                StorageLocations.Add(item);
            }
        }

        private void LoadDynamicFields()
        {
            DynamicFields.Clear();
            if (SelectedAssetType?.MetaDataColumns == null)
                return;

            var fields = _dynamicField.ParseMetaData(SelectedAssetType.MetaDataColumns);
            foreach (var field in fields.Values)
            {
                DynamicFields.Add(new DynamicField
                {
                    Key = field.Key,
                    Control = field.Control,
                    DataType = field.DataType,
                    Value = string.Empty
                });
            }
        }
    }
}
