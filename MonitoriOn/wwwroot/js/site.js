function dropdownToggle() {
    document.getElementById("adminPanelDropDown").classList.toggle("show");
}

window.onclick = function (event) {
    if (!event.target.matches('.settingsMenu-toggle')) {
        var dropdowns = document.getElementsByClassName("settingsMenu-dropDown");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}