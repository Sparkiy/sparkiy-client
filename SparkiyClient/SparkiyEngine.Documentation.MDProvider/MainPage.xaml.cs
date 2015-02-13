using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SparkiyClient.Common.Extensions;
using SparkiyClient.Common.Helpers;
using SparkiyClient.UILogic.Services;
using SparkiyEngine.Bindings.Component.Common;
using SparkiyEngine.Bindings.Component.Common.Attributes;
using SparkiyEngine.Bindings.Component.Graphics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SparkiyEngine.Documentation.MDProvider
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly IStorageService storageService;
        private IReadOnlyDictionary<string, MethodDeclarationDocumentationDetails> data; 


        public MainPage()
        {
            this.InitializeComponent();

            this.storageService = new StorageService();
        }

        private async void MainPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            await this.storageService.InitializeStorageAsync();

            await LoadData();

            await GeneratePages();

            this.StatusTextBlock.Text = "Done";
            this.StatusProgressRing.IsActive = false;
        }

        private async Task LoadData()
        {
             await Task.Run(() => this.data = MethodDeclarationResolver.GenerateDocumentation(
                typeof (IGraphicsBindings), SupportedLanguages.Lua));
        }

        private async Task GeneratePages()
        {
            int counter = 1;
            foreach (var docDetails in this.data.Values)
            {
                await this.Dispatcher.RunAsync(priority: CoreDispatcherPriority.Normal, agileCallback: () =>
                {
                    this.StatusTextBlock.Text =
                        String.Format(
                            "Generating method pages {0}/{1}...",
                            counter++,
                            this.data.Values.Count());
                });

                var pageContent = this.GeneratePage(docDetails);
                var pageUrl = this.GetLink(docDetails);

                await this.WritePageContent(pageUrl, pageContent);
            }

            // Group by categories
            this.StatusTextBlock.Text = "Linking pages...";
            var leafLinkPages = this.data.Values.Select(this.GetLink);
            await this.GenerateLinkPages(leafLinkPages);
            //var groupedInCategories = this.data.Values.GroupBy(details => details.Category).ToDictionary(grouping => grouping.Key, grouping => grouping.AsEnumerable());
            //foreach (var categoryGroup in groupedInCategories)
            //{
            //    await this.Dispatcher.RunAsync(priority: CoreDispatcherPriority.Normal, agileCallback: () =>
            //    {
            //        this.StatusTextBlock.Text =
            //            String.Format(
            //                "Generating link pages {0}/{1}...",
            //                counter++,
            //                groupedInCategories.Keys.Count);
            //    });

            //    var pageContent = this.GenerateLinkPage(categoryGroup.Key, categoryGroup.Value);
            //    var pageUrl = this.GetLinkCategory(categoryGroup.Key);

            //    await this.WritePageContent(pageUrl, pageContent);
            //}
        }

        private async Task GenerateLinkPages(IEnumerable<string> pages)
        {
            var validPages = pages.Where(p => p.LastIndexOf('/') > 0).ToList();
            if (!validPages.Any())
                return;

            var groupedPages = validPages.GroupBy(llp => llp.Substring(0, llp.LastIndexOf('/'))).ToDictionary(grouping => grouping.Key, grouping => grouping);
            foreach (var group in groupedPages)
            {
                if (group.Key == "reference")
                    continue;

                var pageContent = this.GenerateLinkPage(group.Key, group.Value);
                var pageUrl = this.GetLinkCategory(group.Key);

                await this.WritePageContent(pageUrl, pageContent);
            }

            await this.GenerateLinkPages(groupedPages.Keys);
        }

        private async Task WritePageContent(string pageUrl, string pageContent)
        {
            var filePath = this.storageService.WorkspaceFolder.Path + pageUrl.Replace("/", "\\") + ".md";
            var folderPath = Path.GetDirectoryName(filePath);
            StorageFolder currentFolder = this.storageService.WorkspaceFolder;
            foreach (var innerFolder in folderPath.Replace(this.storageService.WorkspaceFolder.Path, "").Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries))
            {
                await currentFolder.EnsureFolderExistsAsync(innerFolder);
                currentFolder = await currentFolder.GetFolderAsync(innerFolder);
            }
            var file = await currentFolder.CreateFileAsync(Path.GetFileName(filePath), CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteTextAsync(file, pageContent);
        }

        private string GenerateLinkPage(string category, IEnumerable<string> items)
        {
            // {0} name
            // {1} generator version
            // {2} generated on date
            // {3} links
            const string page =
                "---\r\n" +
                "title: {0} -- Sparkiy Reference\r\n" +
                "headerActiveClass: reference\r\n" +
                "generatorVersion: {1}\r\n" +
                "generatedDate: {2}\r\n" +
                "---\r\n" +
                "\r\n" +
                "### {0}\r\n" +
                "\r\n" +
                "{3}\r\n";

            const string link = "- [{0}]({1})";

            // Generate links
            StringBuilder sbLinks = new StringBuilder();
            foreach (var detail in items)
                sbLinks.AppendLine(String.Format(link, detail.Split(new [] {'/'}, StringSplitOptions.RemoveEmptyEntries).Last(), this.GetLinkCategory(detail)));

            return String.Format(page,
                category.Split(new []{'/'}, StringSplitOptions.RemoveEmptyEntries).Last(),
                Application.Current.GetVersion(),
                DateTime.UtcNow.ToString("d"),
                sbLinks.ToString());
        }

        private string GeneratePage(MethodDeclarationDocumentationDetails details)
        {
            // {0} name
            // {1} generator version
            // {2} generated on date
            // {3} overloads
            // {4} summary
            // {5} params
            // {6} examples
            // {7} see also
            const string page =
                "---\r\n" +
                "title: {0} -- Sparkiy Reference\r\n" +
                "headerActiveClass: reference\r\n" +
                "generatorVersion: {1}\r\n" +
                "generatedDate: {2}\r\n" +
                "---\r\n" +
                "\r\n" +
                "### {0}\r\n" +
                "\r\n" +
                "{3}" +
                "\r\n" +
                "{4}" +
                "\r\n" +
                "\r\n" +
                "{5}" +
                "\r\n" +
                "{6}" +
                "\r\n" +
                "{7}";

            // {0} name
            const string overloadCall = "    {0}()";

            // {0} name
            // {1} params
            const string overloadSet = "    {0}({1})";

            // {0} return name
            // {1} name
            const string overloadGet = "    {0} = {1}()";

            // {0} return name
            // {1} name
            // {2} params
            const string overloadFunction = "    {0} = {1}({2})";

            // {0} name
            // {1} type
            // {2} description
            const string param = "`{0}` | `{1}` | {2}";

            // {0} image link
            // {1} code
            const string example =
                "```Example\r\n" +
                "{0}\r\n" +
                "====\r\n" +
                "{1}\r\n" +
                "```\r\n";

            // {0} name
            // {1} link
            const string seeAlso = "- [{0}]({1})";


            // Generate see also links
            StringBuilder sbSeeAlso = new StringBuilder();
            if (details.SeeAlso != null)
                foreach (var detailsAlso in details.SeeAlso)
                    sbSeeAlso.AppendLine(String.Format(seeAlso, detailsAlso.Declaration.Name, this.GetLink(details)));

            // Generate examples
            StringBuilder sbExamples = new StringBuilder();
            if (details.Examples != null)
                foreach (var detailsExample in details.Examples)
                    sbExamples.AppendLine(String.Format(example, detailsExample.ImageLink, detailsExample.Code));

            // Generate params
            StringBuilder sbParams = new StringBuilder();
            if (details.Params != null)
                foreach (var detailsParams in details.Params)
                    sbParams.AppendLine(String.Format(param, detailsParams.Name, detailsParams.Type, detailsParams.Description));

            // Generate overloads
            StringBuilder sbOverloads = new StringBuilder();
            if (details.Declaration.Overloads != null)
            {
                foreach (var overload in details.Declaration.Overloads)
                {
                    switch (overload.Type)
                    {
                        case MethodTypes.Call:
                            sbOverloads.AppendLine(String.Format(overloadCall,
                                details.Declaration.Name));
                            break;
                        case MethodTypes.Get:
                            sbOverloads.AppendLine(String.Format(overloadGet,
                                overload.ReturnNames.Aggregate(String.Empty, (s, types) => s + types + ", ").Trim(',', ' '),
                                details.Declaration.Name));
                            break;
                        case MethodTypes.Set:
                            sbOverloads.AppendLine(String.Format(overloadSet,
                                details.Declaration.Name,
                                overload.InputNames.Aggregate(String.Empty, (s, types) => s + types + ", ").Trim(',', ' ')));
                            break;
                        case MethodTypes.Function:
                            sbOverloads.AppendLine(String.Format(overloadFunction,
                                overload.ReturnNames.Aggregate(String.Empty, (s, types) => s + types + ", ").Trim(',', ' '),
                                details.Declaration.Name,
                                overload.InputNames.Aggregate(String.Empty, (s, types) => s + types + ", ").Trim(',', ' ')));
                            break;
                        default:
                            throw new NotSupportedException("Overload type not supported.");
                    }
                }
            }

            return String.Format(page,
                details.Declaration.Name,
                Application.Current.GetVersion(),
                DateTime.UtcNow.ToString("d"),
                sbOverloads.ToString(),
                details.Summary ?? String.Empty,
                sbParams.ToString(),
                sbExamples.ToString(),
                sbSeeAlso.ToString());
        }

        private string GetLinkCategory(string category)
        {
            return "/" + category?.TrimStart('/');
        }

        private string GetLink(MethodDeclarationDocumentationDetails details)
        {
            return "reference/api/" + details.Category?.TrimStart('/') + details.Declaration.Name;
        }
    }
}
