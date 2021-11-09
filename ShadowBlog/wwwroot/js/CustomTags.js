let index = 0

//I need AddTag, DeleteTag, Code for Submitting, Search
function AddTag() {
    let tagEntry = document.getElementById("tagEntry");

    //I will need to make sure that there is Tag text before allowing it to be added.
    let searchResult = Search(tagEntry.value);
    if (searchResult != null) {
        alert(searchResult);
    }
    else {
        //Add the new Tag text as an option in the select
        let newTag = new Option(tagEntry.value, tagEntry.value)
        document.getElementById("TagList").options[index++] = newTag;
    }
    tagEntry.value = "";
    return true;
}

function DeleteTag() {
    //I have to make sure the user selected an option before allowing them to delete
    let tagCount = 1;
    let tagList = document.getElementById("TagList");
    if (!tagList) return false;

    //If nothing is selected throw up an alert
    if (tagList.selectedIndex == -1) {
        alert("A tag must be selected before it can be deleted!");
        return true;
    }
   

    //Else try to delete the selected Tag
    while (tagCount > 0) {
        if (tagList.selectedIndex >= 0) {
            tagList.options[tagList.selectedIndex] = null;
            --tagCount;

        }
        else
            tagCount = 0;
        index--;
    }
    return true;
}

function Search(searchStr) {
    //First I want to make sure they gave me something in the Text tag
    if (searchStr == "") {
        return "Empty tags are unpermitted. Please try again.";
    }

    //I need to make sure the user is not giving a duplicate tag value.
    var tagSelect = document.getElementById("TagList");
    if (tagSelect) {
        let options = tagSelect.options;
        for (let index = 0; index < options.length; index++) {
            if (options[index].value == searchStr) {
                return "Duplicate Tags are not allowed";
            }
        }
    }
}

//JS Code to select all my options during a form submission
document.getElementById("frmCreate").addEventListener("submit", function () {
    // have we been passed an ID 
    Array.from(document.getElementById("TagList").options)
        .forEach(opt => opt.selected = "selected");
    
    //Write the one liner that selects all the options so they can be forwarded to the HtppPost...
    //document.querySelector("#TagList option").prop("selected", "selected");
});