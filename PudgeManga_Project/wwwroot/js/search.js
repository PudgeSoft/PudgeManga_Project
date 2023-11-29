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
function clearInput(){
    var getValue= document.getElementById("inputName");
      if (getValue.value !="") {
          getValue.value = "";
      }
}
/*const multiSelectWithoutCtrl = ( elemSelector ) => {
    let options = [].slice.call(document.querySelectorAll(`${elemSelector} option`));
    options.forEach(function (element) {
        element.addEventListener("mousedown", 
            function (e) {
                e.preventDefault();
                element.parentElement.focus();
                this.selected = !this.selected;
                return false;
            }, false );
    });
  }

  multiSelectWithoutCtrl('#dd')
*/
document.querySelectorAll('.dropdown-menu').forEach(function(element) {
    element.addEventListener('click', function (e) {
        e.stopPropagation();
    });
});
  