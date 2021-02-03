var SiteController = function () {
    this.initialize = function () {
        regsiterEvents();
        loadCart();
    }
    function loadCart() {
        const languageId = $("#currentLang").val();
        $.ajax({
            method: "GET",
            url: "/" + languageId + "/Cart/GetListCart",
            success: function (res) {
                var numberItem = 0;
                $.each(res, function (i, item) {
                    numberItem += item.quantity;
                });
                $("#number_item_header").text(numberItem);
            },
            error: function (e) {
                console.log(e)
            }
        })
    }
    function regsiterEvents() {
        $('body').on("click", '.btn-add-cart', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            const languageId = $("#currentLang").val();

            $.ajax({
                method: "POST",
                url: "/" + languageId + "/Cart/AddToCart",
                data: {
                    id: id,
                    languageId: languageId
                },
                success: function (res) {
                    console.log(res)
                    var numberItem = 0;
                    $.each(res, function (i, item) {
                        numberItem += item.quantity;
                    });
                    $("#number_item_header").text(numberItem);
                },
                error: function (e) {
                    console.log(e)
                }
            })
        });
    }
}

function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}