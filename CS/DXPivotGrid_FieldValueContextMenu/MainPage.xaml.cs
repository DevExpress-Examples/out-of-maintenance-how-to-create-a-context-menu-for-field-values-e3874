using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.PivotGrid;

namespace DXPivotGrid_FieldValueContextMenu {
    public partial class MainPage : UserControl {
        string dataFileName = "DXPivotGrid_FieldValueContextMenu.nwind.xml";
        public MainPage() {
            InitializeComponent();

            // Parses an XML file and creates a collection of data items.
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(dataFileName);
            XmlSerializer s = new XmlSerializer(typeof(OrderData));
            object dataSource = s.Deserialize(stream);

            // Binds a pivot grid to this collection.
            pivotGridControl1.DataSource = dataSource;
        }

        private void CopyValueItem_ItemClick(object sender, ItemClickEventArgs e) {
            PivotGridFieldValueMenuInfo menuInfo =
                pivotGridControl1.GridMenu.MenuInfo as PivotGridFieldValueMenuInfo;
            if (menuInfo != null && menuInfo.ValueItem != null &&
                menuInfo.ValueItem.Value.ToString() != string.Empty)
                Clipboard.SetText(menuInfo.ValueItem.Value.ToString());
        }

        private void FilterValueItem_ItemClick(object sender, ItemClickEventArgs e) {
            PivotGridFieldValueMenuInfo menuInfo =
                pivotGridControl1.GridMenu.MenuInfo as PivotGridFieldValueMenuInfo;
            if (menuInfo != null && menuInfo.ValueItem != null &&
                menuInfo.ValueItem.Value != null &&
                menuInfo.ValueItem.Field != null) {
                PivotGridField field = menuInfo.ValueItem.Field;
                object value = menuInfo.ValueItem.Value;
                field.FilterValues.FilterType = FieldFilterType.Included;
                field.FilterValues.Add(value);
            }
        }
    }
}
