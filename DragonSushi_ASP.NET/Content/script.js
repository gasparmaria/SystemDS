$(document).ready(function () {
    class MobileNavBar {
        constructor(mobileMenu, navList, navLinks) {
            this.mobileMenu = document.querySelector(mobileMenu);
            this.navList = document.querySelector(navList);
            this.navLinks = document.querySelectorAll(navLinks);
            this.activeClass = "active";
            this.overflowY = true;
            this.handleClick = this.handleClick.bind(this);
        }
        hideScrollBar() {
            document.body.style.overflowY = this.overflowY ? "hidden" : "visible";
            this.overflowY = !this.overflowY;
        }
        hideNavList() {
            this.navLinks.forEach((link) => {
                link.addEventListener("click", () => {
                    document.body.style.overflowY = "visible";
                    this.navList.classList.remove(this.activeClass);
                });
            });
        }
        handleClick() {
            this.navList.classList.toggle(this.activeClass);
            this.hideNavList();
            this.hideScrollBar();
        }
        addClickEvent() {
            this.mobileMenu.addEventListener("click", this.handleClick);
        }
        init() {
            if (this.mobileMenu) {
                this.addClickEvent();
            }
            return this;
        }
    }
    const mobileNavBar = new MobileNavBar(
        ".header__mobile-menu",
        ".header__navigation-menu",
        ".header__navigation-menu__list__links__item"
    );
    mobileNavBar.init();

    let searchBarIcon = document.getElementsByClassName("header__search-bar-icon");
    let i;

    for (i = 0; i < searchBarIcon.length; i++) {
        searchBarIcon[i].addEventListener("click", function () {
            this.classList.toggle("active");
            var searchBar = document.querySelector(".search-bar-mobile");
            if (searchBar.style.maxHeight) {
                searchBar.style.maxHeight = null;
            } else {
                searchBar.style.maxHeight = "45px";
            }
        });
    }

    setTimeout(function () {
        let viewheight = $(window).height();
        let viewwidth = $(window).width();
        let viewport = document.querySelector("meta[name=viewport]");
        viewport.setAttribute("content", "height=" + viewheight + "px, width=" + viewwidth + "px, initial-scale=1.0");
    }, 300);

    $(".target").hide();
    $("#div1").show();

    $(".single").click(function () {
        $(".target").hide()
        $("#div" + $(this).attr("target")).show();
    });

    let showRestaurantMenuItem = document.querySelectorAll(".choose-dishe__menu_item");
    showRestaurantMenuItem.forEach(item => {
        item.addEventListener("click", function () {
            showRestaurantMenuItem.forEach(item => item.classList.remove("accent-background"));
            item.classList.add("accent-background");
        });
    });

    let subtractDisheItem = document.querySelectorAll(".amount-dishe-item__subtract");
    subtractDisheItem.forEach(item => {
        item.addEventListener("click", () => {
            let amount = Number(item.nextElementSibling.value);
            if (amount <= 0) {
                amount = 0;
            } else {
                amount = --amount;
            }
            item.nextElementSibling.value = amount;
        });
    });

    let addDisheItem = document.querySelectorAll(".amount-dishe-item__add");
    addDisheItem.forEach(item => {
        item.addEventListener("click", () => {
            let amount = Number(item.previousElementSibling.value);
            item.previousElementSibling.value = ++amount;
        });

    });

    const grabButton = document.getElementsByClassName("button_title");
    let box = document.getElementsByClassName("management_content");
    let button_icon = document.getElementsByClassName("management_button");

    for (let i = 0; i < grabButton.length; i++) {

        grabButton[i].addEventListener(`click`, () => {
            box[i].classList.toggle(`active`)
            if (box[i].classList.contains(`active`)) {
                button_icon[i].innerText = "-"
            }
            else {
                button_icon[i].innerText = "+"
            }
        });
    }
});