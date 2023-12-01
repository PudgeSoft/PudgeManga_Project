function animeButtonSwitch() {
    var element = document.getElementById("animeButton");
    element.classList.toggle("btn-warning");
    element.classList.toggle("btn-outline-warning");
}
function mangaButtonSwitch() {
    var element = document.getElementById("mangaButton");
    element.classList.toggle("btn-warning");
    element.classList.toggle("btn-outline-warning");
}
function clearInput() {
    var getValue = document.getElementById("inputName");
    if (getValue.value != "") {
        getValue.value = "";
    }
}
document.querySelectorAll('.dropdown-menu').forEach(function (element) {
    element.addEventListener('click', function (e) {
        e.stopPropagation();
    });
});