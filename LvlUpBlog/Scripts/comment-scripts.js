$(document).ready(function () {
   
    $("a[data-comment-delete]").click(function (e) {

        e.preventDefault(); 

        var message = $(this).data("comment-delete");

        // Check if message exists and ask user to confirm
        if (message != "") {
            if (!confirm(message))
                return;
        }

        /* Post request synchronously
        -------------------------------------------------------------- */
        $("<form>").attr("method", "post")
                    .attr("action", $(this).attr("href"))
                    .appendTo(document.body)
                    .submit();
       
    });
}); 