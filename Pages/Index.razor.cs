using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;

namespace Dankwms.Pages
{
    public partial class Index
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        protected System.Collections.Generic.List<Dankwms.WmsEntry> wmsEntries;

        public string search;
        public string imgPath;

        public string id;
        public string name;
        public string location;
        public string description;
        public string keywords;
        public string picture;
        public string lastSql;

        public List<WmsEntry> RefreshGrid() {
            string sql = new("SELECT * FROM assets");
            //lastSql = sql;
            return (WmsSQL.Command(sql));
        }

        protected override async Task OnInitializedAsync()
        {
            wmsEntries = RefreshGrid();
        }

        protected async System.Threading.Tasks.Task Button0Click(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            string sql = new($"SELECT * FROM assets WHERE ID LIKE '%{search}%' OR NAME LIKE '%{search}%' OR DESCRIPTION LIKE '%{search}%' OR LOCATION LIKE '%{search}%' OR KEYWORDS LIKE '%{search}%'");
            wmsEntries = WmsSQL.Command(sql);
            lastSql = sql;
        }

        protected async System.Threading.Tasks.Task DataGrid0RowSelect(Dankwms.WmsEntry args)
        {
            string filePath = args.Picture;
            imgPath = WmsSQL.GetImageBase64(filePath);

            id = args.Id;
            name = args.Name;
            location = args.Location;
            description =args.Description;
            keywords = args.Keywords;
            picture = args.Picture;
        }

        public void UpdateEntry() {
            string sql = new($"UPDATE assets SET name='{name}', description='{description}',location='{location}', picture='{picture}', keywords='{keywords}' WHERE ID='{id}'");
            WmsSQL.Command(sql);
            lastSql = sql;
            wmsEntries = RefreshGrid();
        }

        protected async System.Threading.Tasks.Task updateButtonClick(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            UpdateEntry();
        }
    }
}