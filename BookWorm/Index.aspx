<%@ Async="true" Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="BookWorm.Index" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta charset="utf-8" />
	<link href="styles.css" rel="stylesheet" />
	<script src="Scripts/jquery-3.4.1.min.js"></script>
	<script src="Scripts/BookwormApp.js" type="text/javascript"></script>
	<link href="https://fonts.googleapis.com/css?family=Montserrat&display=swap" rel="stylesheet"/> 
	<title>Bookworm - text analyser app</title>
</head>
<body runat="server"> 
	<!-- HEADER -->
	<form id="MyForm" runat="server">
		<div id="Header" class="header">
			<div> 
				<a onclick="ShowAboutWindow()">About</a>
			</div>
			<!-- TO BE DEVELOPED ;) -->
			<div style="display: none">
				<a>En/Ru</a>
			</div>
		</div>
		
		<!-- ABOUT WINDOW -->
		<div id="AboutWindow" class="aboutWindow" runat="server" style="display: none">
		  <!-- Modal content -->
		  <div class="aboutWindow-content">
			<span class="aboutWindowClose">&times;</span>
			<p id="exceptionMessage">
				 BookWorm is an text analysis app
				 It searches free book site gutenberg.org(with site's own search engine) and then performs text analysis,
				 based on frequency of word appearance. You can search for one word or exact phrase - algorithm will
				 show you the next most frequent word and will build sentence with next words according to frequency
				 of their appearance. </p>
			<p> For example, phrase "No sentence ends with because, because, because is a conjuction - deal with it!" 
				will return "because" for "with" query: we have three "because" after "with" - and only one "it". </p>
			<p> I implemented this algorithm as a part of C# online course on ulearn.me some time ago and 
				added some online functionality to it, like website scrapping and text downloading</p>
		  </div>
		</div> 
	
	<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
	
		<!-- DEBUG BUTTONS -->
		<asp:UpdatePanel runat="server" DefaultButton="BookSearchButton" ID="UpdatePanel1" style="display: none">
			<ContentTemplate>
				<div class="helpButtons" id="HelpButtons" style="display: normal">
					<asp:button id="ShowAllButton" runat="server" Text="Show exception!" onclick="ShowAll_Click"> </asp:button>
					<asp:Button id="Refresh" runat="server" Text="Refresh" onclick="Page_Refresh"/>
				</div>
			</ContentTemplate>
		</asp:UpdatePanel>
		
		<!-- SEARCH PANEL -->
		<asp:UpdatePanel runat="server" DefaultButton="BookSearchButton" ID="UpdatePanelBookSearch">
			<ContentTemplate>
				<div class="wrapper-search" id="SearchBarDiv" style="display: normal"  runat="server">
					<h1>Bookworm</h1>
					<h2 id="HeaderSearchQuery" runat="server">Search for a book or author:</h2>
					<h3 > <a href="http://www.gutenberg.org" class="gutenbergLink" target="_blank">at Project Gutenberg Library </a> </h3>
					<div class="divBookSearchForm">
						<asp:textbox id="BookSearchForm" class="searchform-book" type="text" placeholder="For example, Dracula" runat="server" name="bookSearchQuery" onkeypress="return bookSearchFormKeyPress(event)" CssClass="aspTextBox"></asp:textbox>
						<asp:button id="BookSearchButton" class="aspButton" text="Search" onclick="BookSearchButton_Click" runat="server"></asp:button>
					</div>
					<p id="SearchInProgressMessage" class="searchInProgressMessage" runat="server" style="visibility: visible">Hint: an empty query will return list of most popular books</p>
				</div>
			<asp:UpdateProgress ID="UpdateProgressBookSearch" runat="server" AssociatedUpdatePanelID="UpdatePanelBookSearch">
				<ProgressTemplate>
					<img src="Media/worm_progress.gif"/>
				</ProgressTemplate>
			</asp:UpdateProgress>
		</ContentTemplate>
		</asp:UpdatePanel>

		<!-- BOOK SEARCH RESULTS -->
		<asp:UpdatePanel runat="server" class="bookSearchResults" ID="UpdatePanelBookSearchResults">
		<ContentTemplate>
			<div  id="BookSearchResultsDiv" style="display: normal" runat="server">
				<asp:ListBox id="BooksList" class="booksList" runat="server" onkeypress="return booksListKeyPress(event)" CssClass="aspTextBox"></asp:ListBox>
				<div class="booksListSelectionButtonDiv">
					<div>
						<asp:button id="BooksListSelectionButton" class="aspButton" onclick="BookListSelectionButton_Click"  runat="server" text="Analyze this book"></asp:button>
					</div>
					<div>
						<asp:Button ID="ButtonStartAgain1" class="aspButton" runat="server" Text="Find a new book" OnClick="Page_Refresh"/>
					</div>
				</div>
			</div>

			<!-- PROGRESS BAR -->
			<asp:UpdateProgress class="updateProgressBookSelection" ID="UpdateProgressBookSelection" runat="server" AssociatedUpdatePanelID="UpdatePanelBookSearchResults">
				<ProgressTemplate>
						<img src="Media/worm_progress.gif" />
				</ProgressTemplate>
			</asp:UpdateProgress>
		</ContentTemplate>
		</asp:UpdatePanel>
		
		<!-- TEXT ANALYSIS -->
		<asp:UpdatePanel runat="server" >
		<ContentTemplate>
			<div class="wrapper-results-view" id="ResultsDiv" style="display: normal" runat="server">
				<div>
					<h2 id="WordSearchHeader" runat="server">Type the word or phrase to generate sentence:</h2>
					<asp:TextBox class="wordSearchForm" ID="WordSearchForm" runat="server" onkeypress="return wordSearchFormKeyPress(event)" CssClass="aspTextBox"></asp:TextBox>
					<asp:Button ID="Button1" class="aspButton" runat="server" Text="Search" OnClick="ButtonAnalyze_Click"/>
				</div>
				<div class="resultTextDiv">
					<asp:Literal ID="GeneratedSentence"  runat="server"></asp:Literal>
				</div>
				<div class="resultTextButtons">
					<div>
						<asp:Button ID="ButtonBackToList" class="aspButton" runat="server" Text="Back to book list" OnClick="ButtonBackToList_Click"/>
					</div>
					<div>
						<asp:Button ID="ButtonStartAgain" class="aspButton" runat="server" Text="Find a new book" OnClick="Page_Refresh"/>
					</div>
				</div>
			</div>
		</ContentTemplate>
		</asp:UpdatePanel>
	</form>
</body>
</html>