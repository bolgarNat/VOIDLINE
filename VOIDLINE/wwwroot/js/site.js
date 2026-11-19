// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const sidebar = document.getElementById("sidebar");

const add_button_container = document.getElementById("add_button_container");

const add_button = document.getElementById("add_button");

const container_change_amount = document.getElementById("container-change-amount");

function sidebar_change() {
    sidebar.classList.toggle("active");
}

function change_amount() {
    add_button_container.replaceChildren();
    container_change_amount.style.visibility = "visible";
}