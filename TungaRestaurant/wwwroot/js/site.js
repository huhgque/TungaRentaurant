var host = window.location.origin;

$(".redirect-on-click").on("click", function () {
    console.log(host);
    var redirectUrl = $(this).attr("href");
    
    window.location.href = host + "/" + redirectUrl;
})