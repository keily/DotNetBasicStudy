/*
$(document).ready(function () {
    console.log("ready!");
});
*/
jQuery(function () {
    console.log("ready!");
    $("#main").html("<p>sss</p>");
    $("#main p").addClass("background-color:red;");
    //$("<p style=\"background-color:red;\">123</p>").appendTo("#main");

    var $myhref = $("<a/>", {
        html: "This is a <strong>new</strong> link",
        "class": "new",
        href: "foo.html"
    });
    $myhref.insertAfter($("#main p").get(0));
    $("#main a:first").attr("href", "hao123.htm");

    var myItems = [];
    for (var i = 0; i < 5; i++) {
        myItems.push("<li>item" + i + "</li>");
    }
    $("#main").append(myItems.join(""));
    $("#main li").each(function () {
        //console.log(this);
    });
    $("#main li").hover(function () {
        $(this).toggleClass("lihover");
    });

    var main = document.getElementById("main"); //$("#main");
    var newdiv = document.createElement("div");
    newdiv.setAttribute("id", "newdiv");
    main.parentNode.insertBefore(newdiv, main);
    $(newdiv).html("<h1>aaa</h1>");
    $(newdiv).css({
        fontSize: "20px",
        color: "red"
    });
    $(newdiv).height("50px");
});