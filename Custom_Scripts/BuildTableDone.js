$(document).ready(function () {



    $.ajax({
        url: "/ToDoes/BuildDoneTable",
        success: function (result) {

            $("#tableDiv").html(result);
        }
    })
})