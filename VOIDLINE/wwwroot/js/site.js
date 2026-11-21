// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const sidebar = document.getElementById("sidebar");

const fp_modal = document.getElementById("fp_modal");
const cv_modal = document.getElementById("cv_modal");

const add_button_container = document.getElementById("add_button_container")

const container_change_amount = document.getElementById("container-change-amount");

function sidebar_change() {
    sidebar.classList.toggle("active");
}

function change_fp_modal_visability() {
    fp_modal.classList.toggle("active");
}
function change_cv_modal_visability() {
    cv_modal.classList.toggle("active");
}

function send_form() {
    resend_code_form = document.getElementById("resend_code");
    resend_code_form.submit();
}

function change_amount() {
    add_button_container.replaceChildren();
    container_change_amount.style.visibility = "visible";
}