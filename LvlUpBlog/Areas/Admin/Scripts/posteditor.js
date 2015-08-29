$(document).ready(function () {
    
    var $tagEditor = $(".post-tag-editor");

    $tagEditor
        .find(".tag-select")
        .on("click", "> li > a", function (e) {         // Attach click handler to tag-select class. Click event generated from li > a descendant      
            
            e.preventDefault(); 
            var $tagParent = $(this).closest("li");
            $tagParent.toggleClass("selected"); 

            var selected = $tagParent.hasClass("selected");
            $tagParent.find(".selected-input").val(selected);
        });

    var $addTagButton = $tagEditor.find(".add-tag-button");
    var $newTagName = $tagEditor.find(".new-tag-name");

    // Add tag button
    $addTagButton.click(function (e) {
        e.preventDefault();
        addTag($newTagName.val()); 
    });

    // Input for entering new tag name
    $newTagName
        .keyup(function (e) {

            if ($newTagName.val().trim().length > 0)
                $addTagButton.prop("disabled", false);      // If text in new tag text box, enable add tag button
            else
                $addTagButton.prop("disabled", true);
        })
        .keydown(function (e) {
            if (e.which == 13)          // e.which = 13 = enter key, call addTag function
            {
                e.preventDefault();
                addTag($newTagName.val());
            }
        });

    function addTag(name) {

        // find next index for tags. Will be number of li elements - 1 since have tags + 1 li elements (+1 because of template).
        var newIndex = $tagEditor.find(".tag-select > li").size() - 1;

        // Clone template tag. Insert new tag values into cloned template. Insert template at end of tagEditor
        $tagEditor
            .find(".tag-select > li.template")
            .clone()
            .removeClass("template")
            .addClass("selected")
            .find(".name").text(name).end()
            .find(".name-input").val(name).attr("name", "Tags[" + newIndex + "].Name").end()
            .find(".selected-input").attr("name", "Tags[" + newIndex + "].IsChecked").val(true).end()
            .appendTo($tagEditor.find(".tag-select"));

        // Reset add tag text box
        $newTagName.val("");
        $addTagButton.prop("disabled", true);

    }

});