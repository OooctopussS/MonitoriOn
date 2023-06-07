function dropdownToggle() {

    var userDropdown = document.getElementById("userPanelDropdown");

    if (userDropdown.classList.contains('show'))
    {
        userDropdown.classList.toggle('show');
    }

    if (document.getElementById("adminPanelDropdown") != null) {
        document.getElementById("adminPanelDropdown").classList.toggle("show");

    }
}

function userdropdownToggle() {

    var adminDropdown = document.getElementById("adminPanelDropdown");

    if (adminDropdown != null) {
        if (adminDropdown.classList.contains('show')) {
            adminDropdown.classList.toggle('show')
        }
    }
    document.getElementById("userPanelDropdown").classList.toggle("show");
}

window.onclick = function (event) {
    if (!event.target.matches('.settingsMenu-toggle')) {
        var dropdowns = document.getElementsByClassName("panel-dropdown");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}