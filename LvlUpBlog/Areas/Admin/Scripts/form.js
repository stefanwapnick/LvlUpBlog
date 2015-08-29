$(document).ready(function () {
   
    $("a[data-post]").click(function (e) {
        
        e.preventDefault(); 

        var message = $(this).data("post");

        // Check if message exists and ask user to confirm
        if (!confirm(message))
            return;

        // Submit post request using path in href
        /* $.ajax({
            type : 'POST',    
            url : $(this).attr("href"), 
            async : false
        });  */
        /* Post request synchronously
        -------------------------------------------------------------- */
        $("<form>").attr("method", "post")
                    .attr("action", $(this).attr("href"))
                    .appendTo(document.body)
                    .submit();
       
    });

    // Generate slug for title
    $("[data-slug]").each(function(){

        var $slugTextBox = $(this)
        var $sendSlugFrom = $($slugTextBox.data("slug"));           // Get title field in edit posts form data-slug="#Title will pass id of slug field

        $sendSlugFrom.keyup(function () {
            var slug = $sendSlugFrom.val();
            
            slug = slug.replace(/[^a-zA-Z0-9\s]/g, "");     // replace everything that is not alphanumeric or space with empty string
            slug = slug.toLowerCase();
            slug = slug.replace(/\s+/g, "-");               // replace all whitespace characters with a dash

            if (slug.charAt(slug.lngth - 1) == "-")         // remove last character if it is a dash
                slug = slug.substr(0, slug.lenth - 1);      

            $slugTextBox.val(slug);

        });

    });

}); 