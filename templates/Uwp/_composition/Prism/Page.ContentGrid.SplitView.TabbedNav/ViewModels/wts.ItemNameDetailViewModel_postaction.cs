﻿namespace Param_ItemNamespace.ViewModels
{
    public class wts.ItemNameDetailViewModel : ViewModelBase
    {
        public SampleOrder Item
        {
            get { return _item; }
            set { SetProperty(ref _item, value); }
        }
//{[{

        public wts.ItemNameDetailViewModel(ISampleDataService sampleDataServiceInstance, IConnectedAnimationService connectedAnimationService)
        {
            // TODO WTS: Replace this with your actual data
            _sampleDataService = sampleDataServiceInstance;
            _connectedAnimationService = connectedAnimationService;
        }
//}]}
    }
}

