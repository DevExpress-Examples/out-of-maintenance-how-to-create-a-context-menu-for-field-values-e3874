Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Controls
Imports System.Xml.Serialization
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.PivotGrid

Namespace DXPivotGrid_FieldValueContextMenu
	Partial Public Class MainPage
		Inherits UserControl
		Private dataFileName As String = "nwind.xml"
		Public Sub New()
			InitializeComponent()

			' Parses an XML file and creates a collection of data items.
			Dim [assembly] As System.Reflection.Assembly = _
				System.Reflection.Assembly.GetExecutingAssembly()
			Dim stream As Stream = [assembly].GetManifestResourceStream(dataFileName)
			Dim s As New XmlSerializer(GetType(OrderData))
			Dim dataSource As Object = s.Deserialize(stream)

			' Binds a pivot grid to this collection.
			pivotGridControl1.DataSource = dataSource
		End Sub

		Private Sub CopyValueItem_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
			Dim menuInfo As PivotGridFieldValueMenuInfo = _
				TryCast(pivotGridControl1.GridMenu.MenuInfo, PivotGridFieldValueMenuInfo)
			If menuInfo IsNot Nothing AndAlso _
					menuInfo.ValueItem IsNot Nothing AndAlso _
					menuInfo.ValueItem.Value.ToString() <> String.Empty Then
				Clipboard.SetText(menuInfo.ValueItem.Value.ToString())
			End If
		End Sub

		Private Sub FilterValueItem_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
			Dim menuInfo As PivotGridFieldValueMenuInfo = _
				TryCast(pivotGridControl1.GridMenu.MenuInfo, PivotGridFieldValueMenuInfo)
			If menuInfo IsNot Nothing AndAlso _
					menuInfo.ValueItem IsNot Nothing AndAlso _
					menuInfo.ValueItem.Value IsNot Nothing AndAlso _
					menuInfo.ValueItem.Field IsNot Nothing Then
				Dim field As PivotGridField = menuInfo.ValueItem.Field
				Dim value As Object = menuInfo.ValueItem.Value
				field.FilterValues.FilterType = FieldFilterType.Included
				field.FilterValues.Add(value)
			End If
		End Sub
	End Class
End Namespace
