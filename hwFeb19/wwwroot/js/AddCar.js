$(() => {
    $("input").on('input', function () {
        validateEntry();
    });

    $("#car-type").on('change', function () {
        validateEntry();
        if ($(this).val() == 2) {
            $("#has-leather-seats").prop('checked', true);
            $("#has-leather-seats").prop('disabled', true);
        }
    });

    $("#submit-button").on('click', function () {
        $("#has-leather-seats").prop('disabled', false);
    })

    function validateEntry() {
        const make = $("#make").val().trim();
        const model = $("#model").val().trim();
        const year = $("#year").val();
        const price = $("#price").val();
        const type = $("#car-type").val() >= 0;
        const isValid = make && model && year && !isNaN(year) && price && !isNaN(price) && type;

        $("#submit-button").prop('disabled', !isValid);
    }
})