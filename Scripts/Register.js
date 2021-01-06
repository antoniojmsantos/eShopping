document.getElementById("div-empresa").style.visibility = "hidden";

function roleChange() {
    if (document.getElementById("dropdown-role").value == "Empresa") {
        document.getElementById("div-empresa").style.visibility = "visible";
        document.getElementById("nome-empresa").required = true;
    }
    else {
        document.getElementById("div-empresa").style.visibility = "hidden";
        document.getElementById("nome-empresa").required = false;
    }
        
}