function ChangeName() {
	console.log("Here!");
	document.getElementById("BookSearchButton").value = "Working...";
}

	
$(document).ready(function () {
	console.info("I am ready to serve, Master!");
});
var countdown = 5;

function ShowAboutWindow() {
	var modal = document.getElementById("AboutWindow");
	var span = document.getElementsByClassName("aboutWindowClose")[0];
	modal.style.display = "block";

	span.onclick = function () {
		modal.style.display = "none";
	}

	window.onclick = function (event) {
		if (event.target == modal) {
			modal.style.display = "none";
		}
	}
}


function timerCount() {
	myVar = setInterval(doCountdown, 1000);
}

function doCountdown() {
	countdown--;
	if (countdown === 0) {
		window.open("Index.aspx", "_self");
	}
	else {
		document.getElementById("countdownText").innerHTML = "Return to homepage in " + countdown;
	}
}	

function bookSearchFormKeyPress(e) {
	e = e || window.event;
	var key = e.keyCode;
	if (key == 13) 
	{
		document.getElementById("BookSearchButton").value = "Working...";
		document.getElementById("BookSearchButton").focus();
		document.getElementById("BookSearchButton").click();
		return false;
	}
}

function wordSearchFormKeyPress(e) {
	e = e || window.event;
	var key = e.keyCode;
	if (key == 13) 
	{
		document.getElementById("Button1").value = "Working...";
		document.getElementById("Button1").focus();
		document.getElementById("Button1").click();
		
		return false;
	}
}

function booksListKeyPress(e) {
	e = e || window.event;
	var key = e.keyCode;
	if (key == 13)
	{
		document.getElementById("BooksListSelectionButton").value = "Working...";
		document.getElementById("BooksListSelectionButton").focus();
		document.getElementById("BooksListSelectionButton").click();
		return false;
	}
}

