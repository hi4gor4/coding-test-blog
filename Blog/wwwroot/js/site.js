$(document).ready(function () {
    $('.cpf-mask').mask('000.000.000-00');
    $('.phone-mask').mask('(00) 00000-0000');

    flatpickr('.datepicker', {
        dateFormat: "d/m/Y",
        maxDate: "today",
        locale: "pt"
    });

    $('#confirmPassword').on('keyup', function () {
        const password = $('#password').val();
        const confirmPassword = $(this).val();

        if (password !== confirmPassword) {
            this.setCustomValidity("As senhas não coincidem!");
        } else {
            this.setCustomValidity("");
        }
    });
});