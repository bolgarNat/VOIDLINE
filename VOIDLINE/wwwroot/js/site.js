// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const sidebar = document.getElementById("sidebar");

const fp_modal = document.getElementById("fp_modal");
const cv_modal = document.getElementById("cv_modal");
const np_modal = document.getElementById("np_modal");

const add_button_container = document.getElementById("add_button_container")

const container_change_amount = document.getElementById("container-change-amount");

$(function () {
    'use strict';

    var form = $('#codeForm');

    function goToNextInput(e) {
        var key = e.which,
            t = $(e.target),
            sib = t.next('input');

        if (key != 9 && (key < 48 || key > 57)) {
            e.preventDefault();
            return false;
        }

        if (key === 9) {
            return true;
        }

        if (sib.length) {
            sib.select().focus();
        }
    }

    function onKeyDown(e) {
        var key = e.which;

        if (key === 9 || (key >= 48 && key <= 57)) {
            return true;
        }

        e.preventDefault();
        return false;
    }

    function onFocus(e) {
        $(e.target).select();
    }

    form.on('keyup', 'input', goToNextInput);
    form.on('keydown', 'input', onKeyDown);
    form.on('click', 'input', onFocus);
});

function sidebar_change() {
    sidebar.classList.toggle("active");
}

function change_fp_modal_visability() {
    fp_modal.classList.toggle("active");
}

function change_cv_modal_visability() {
    cv_modal.classList.toggle("active");
}

function change_np_modal_visability() {
    np_modal.classList.toggle("active");
}

function resend_code_form() {
    resend_code_form = document.getElementById("resend_code");
    resend_code_form.submit();
}

function resend_email_form() {
    resend_email_form = document.getElementById("change_email");
    resend_email_form.submit();
}

function change_amount() {
    add_button_container.replaceChildren();
    container_change_amount.style.visibility = "visible";
}