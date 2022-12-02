using CommunityToolkit.Mvvm.ComponentModel;

namespace CRTWorldEditor.Datas
{
    public partial class ProjectViewData : ObservableObject
    {
        [ObservableProperty]
        public string name;

        [ObservableProperty]
        public string platform;

        [ObservableProperty]
        public string lastOpenTime;
    }
}
