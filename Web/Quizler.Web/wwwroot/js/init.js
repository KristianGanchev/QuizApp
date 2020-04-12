(function ($) {
    $(function () {

        $('.sidenav').sidenav();

    }); // end of document ready

    $(document).ready(function () {
        $('select').formSelect();
    });

    $(document).ready(function () {
        $('.modal').modal();
    });

    $(document).ready(function () {
        $('.collapsible').collapsible();
    });

})(jQuery); // end of jQuery name space

