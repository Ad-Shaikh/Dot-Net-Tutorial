function deletePost(id) {
    var ask = window.confirm("Are you sure you want to delete?");
    if (ask) {

        window.alert("Employee was successfully deleted.");

        window.location.href = "/Employee/Delete?id="+id;

    }
}

function Submit() {
    var ask = window.confirm("Are you sure you want to Submit?");
    if (ask) {

        window.alert("Employee was Added successfully");

    }
}

function Edit() {
    var ask = window.confirm("Are you sure?");
    if (ask) {

        window.alert("Employee was Edited successfully");

    }
}


function openNav() {
    document.getElementById("mySidenav").style.width = "250px";
}

function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
}

