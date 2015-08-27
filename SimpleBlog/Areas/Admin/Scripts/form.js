$(document).ready(function () {

    $("a[data-post]").click(function (e) {
        
        e.preventDefault(); 

        var message = $(this).data("post");

        // Check if message exists and ask user to confirm
        if (!confirm(message))
            return;

        // Submit post request using path in href
        $.post($(this).attr("href"));

        /* Alternative method to do post request
        --------------------------------------------------------------
        $("<form>").attr("method", "post")
                    .attr("action", $(this).attr("href"))
                    .appendTo(document.body)
                    .submit();
       */
    }); 
}); 