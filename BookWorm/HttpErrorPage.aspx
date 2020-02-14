<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HttpErrorPage.aspx.cs" Inherits="BookWorm.HttpErrorPage" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
	<link href="ErrorPage.css" rel="stylesheet"/>
	<script src="Scripts/jquery-3.4.1.min.js"></script>
	<script src="Scripts/BookwormApp.js" type="text/javascript"></script>
	<title>Bookworm error page</title>
</head>
<body>
    <form id="form1" runat="server">
		<script>
			var countdown = 5;
			$(document).ready(
				function timerCount() {
					myVar = setInterval(doCountdown, 1000);
				})
				function doCountdown() {
					countdown--;
					if (countdown === 0) {
						window.open("Index.aspx", "_self");
					}
					else {
						document.getElementById("countdownText").innerHTML = "Return to homepage in " + countdown +"...";
					}
				}	
		</script>
        <div class="container">
             
        <div class="content-wrapper">  
               <div class="two-column-wrapper">
                   <div class="profile-image-wrapper">
                        <img src="Media/errorimg.png" />
                   </div>
                   <div class="profile-content-wrapper">
                       <h1 meta:resourcekey="lblErrorHeader">WE HAVE AN ERROR HERE!</h1>
                       <p meta:resourcekey="lblCountdown" id="countdownText"> Return to homepage in 5...</p>
                       
                   </div>
               </div>
        </div>
    </div>
    </form>
</body>
</html>

<script>
	

</script>