using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;

namespace ExXaml.Test
{
    public static class XamlDocumenttExtensions
    {
        public static XDocument ToMicrosoftXaml(this XDocument xdoc)
        {
            return xdoc;
        }
        public static XDocument ToXamarinXaml( this XDocument xdoc )
        {
            return xdoc;
        }
    }
    [TestClass]
    public class TestMicrosoftXaml
    {
        [TestMethod]
        public void TestMethod1()
        {
            string xaml = @"
<Page>
    <Grid>
	    <Grid.ColumnDefinitions>
		    <ColumnDefinition Width=""1*""/>
		    <ColumnDefinition Width=""1*""/>
		    <ColumnDefinition Width=""1*""/>
	    </Grid.ColumnDefinitions>
	    <Grid.RowDefinitions>
		    <RowDefinition Height=""*""/>
		    <RowDefinition Height=""*""/>
	    </Grid.RowDefinitions>
		<UILabel Text=""セル"" Grid.Column=""0"" Grid.Row=""0"" />
</Page>";

        }
    }
}
