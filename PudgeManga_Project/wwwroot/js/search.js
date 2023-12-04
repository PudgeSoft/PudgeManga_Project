function animeButtonSwitch() {
    var element = document.getElementById("animeButton");
    element.classList.add("btn-warning");
    element.classList.remove("btn-outline-warning");
    var element = document.getElementById("mangaButton");
    element.classList.remove("btn-warning");
    element.classList.add("btn-outline-warning");
}
function mangaButtonSwitch() {
    var element = document.getElementById("mangaButton");
    element.classList.add("btn-warning");
    element.classList.remove("btn-outline-warning");
    var element = document.getElementById("animeButton");
    element.classList.remove("btn-warning");
    element.classList.add("btn-outline-warning");
}
if (window.location.href.includes('Anime')) {
    animeButtonSwitch();
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