let sidebar = document.querySelector(".sidebar");
let closeBtn = document.querySelector("#btn");
let searchBtn = document.querySelector(".bx-search");
let container = document.querySelector(".container");
let actualform = document.querySelector("#actualform");

closeBtn.addEventListener("click", () => {
	sidebar.classList.toggle("open");
	menuBtnChange();//calling the function(optional)
});

// following are the code to change sidebar button(optional)
function menuBtnChange() {
	if (sidebar.classList.contains("open")) {
		closeBtn.classList.replace("bx-menu", "bx-menu-alt-right");//replacing the iocns class
		//container.style.margin = "auto";
		container.style.marginLeft = "14rem";
		actualform.style.width = "156%";

	} else {
		closeBtn.classList.replace("bx-menu-alt-right", "bx-menu");//replacing the iocns class
		//container.style.width = "70rem";
		container.style.marginLeft = "5rem";
		actualform.style.width = "185%";
	}
}