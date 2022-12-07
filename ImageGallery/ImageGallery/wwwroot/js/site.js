// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
async function deletePost(id, filename) {
    var ask = window.confirm("Are you sure you want to delete?");
    if (ask) {
        window.alert("Data was successfully deleted.");

        window.location.href = "/Gallery/Delete/?ID="+id+"&filename="+filename;

    }
}

function Submit() {
    var ask = window.confirm("Are you sure you want to Submit?");
    if (ask) {

        window.alert("Data was Added successfully");

    }
}


function confirmSubmit() {
    var agree = confirm("Are you sure you wish to Submit?");
    if (agree)
        return true;
    else
        return false;
}