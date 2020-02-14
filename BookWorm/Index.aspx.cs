using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookWorm
{
    public partial class Index : System.Web.UI.Page
    {
        protected void ShowSearchResults()
        {
            SearchBarDiv.Style.Value = "display: none";
            BookSearchResultsDiv.Style.Value = "display: normal";
            ResultsDiv.Style.Value = "display: none";
        }

        protected void ShowAnalyzeForm()
        {
            SearchBarDiv.Style.Value = "display: none";
            BookSearchResultsDiv.Style.Value = "display: none";
            ResultsDiv.Style.Value = "display: normal";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SearchBarDiv.Style.Value = "display: normal";
            BookSearchResultsDiv.Style.Value = "display: none"; 
            ResultsDiv.Style.Value = "display: none";
        }

        protected async void BookSearchButton_Click(object sender, EventArgs e)
        {
            List<Book> Books = new List<Book>();
            try
            {
                Books = await WebTasks.FindBooks(BookSearchForm.Text);
            }
            catch (Exception exception)
            {
                ExceptionHandler(exception);
                throw new Exception();
            }
            Session["books"] = Books;
            if (Books.Count > 0)
            {
                ShowSearchResults();
                BooksList.DataSource = Books;
                BooksList.DataBind();
                BooksList.Rows = Math.Max(6, Books.Count / 4);
                BooksList.SelectedIndex = 0;
            }
            else SearchInProgressMessage.InnerHtml = "No results found. Try again!";
        }

        protected async void BookListSelectionButton_Click(object sender, EventArgs e)
        {
            var index = BooksList.SelectedIndex;
            var books = (List<Book>)Session.Contents["books"];
            var bookToDownload = new Book(null, null,null);
            try
            {
                bookToDownload = books.ElementAt(index);
            }
            catch(Exception exception){
                ExceptionHandler(exception);
            }
            Book downloadedBook = null;
            if (bookToDownload != null)
            {
                try
                {
                    downloadedBook = await WebTasks.DownloadText(bookToDownload);
                }
                catch(Exception exception) {
                    ExceptionHandler(exception);
                    throw new Exception();
                }
            }
            if (downloadedBook != null)
            {
                var sentences = SentencesParserTask.ParseSentences(downloadedBook.Text);
                var frequencyDictionary = FrequencyAnalysisTask.GetMostFrequentNextWords(sentences);
                Session["dictionary"] = frequencyDictionary;
                ShowAnalyzeForm();
            }
            else ExceptionHandler(new NullReferenceException("Book link returned null"));
        }

        protected void ButtonAnalyze_Click(object sender, EventArgs e)
        {
            ShowAnalyzeForm();
            GeneratedSentence.Text = "";
            Dictionary<string,string> dictionary = (Dictionary<string, string>) Session["dictionary"];
            string word = WordSearchForm.Text;
            if (string.IsNullOrWhiteSpace(word) || string.IsNullOrEmpty(word))
            {
                WordSearchForm.Text = null;
                GeneratedSentence.Text = "You can't analyze an empty query";
            }
            else
            {
                var phrase = TextGeneratorTask.ContinuePhrase(dictionary, word.ToLower(), 10);
                if (phrase == word.ToLower()) phrase = "Search returned zero matches. Try again!";
                GeneratedSentence.Text = phrase;
            }
            WordSearchForm.Text = "";
        }

        protected void ButtonBackToList_Click(object sender, EventArgs e)
        {
            GeneratedSentence.Text = "";
            ShowSearchResults();
        }

        
        protected void Page_Refresh(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        
        //Visibility debugging method
        protected void ShowAll_Click(object sender, EventArgs e)
        {
            ExceptionHandler(new Exception("Boo!"));
        }

        //Debug method for exception handling
        public void ExceptionHandler(Exception exception)
        {
            ScriptManager.RegisterStartupScript(this,
                GetType(), "",
                "Myfunction(\"" + exception.Message + "\")",
                true);
        }
    }
}