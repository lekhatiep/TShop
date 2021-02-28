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
            productId = id;
            const languageId = $("#currentLang").val();
            var x = $(this).offset().top;
            var qtyItem = document.getElementById("qty-add-item").value;

            $.ajax({
                method: "POST",
                url: "/" + languageId + "/Cart/AddToCart",
                data: {
                    id: id,
                    languageId: languageId,
                    quan: qtyItem
                },
                success: function (res) {
                    console.log(res)
                    if (res === "101") {
                        $('.add-cart-msg-alert').hide()
                            .addClass('alert alert-danger')
                            .fadeIn('300').fadeOut(4500);
                    } else {
                        var numberItem = 0;

                        $('html,body').animate({ scrollTop: x - 800 }, 2000);

                        $.each(res, function (i, item) {
                            numberItem += item.quantity;
                        });
                        $("#number_item_header").text(numberItem);
                        $('.add-cart-msg').hide()
                            .fadeIn('1000').fadeOut(5000);
                    }
                },
                error: function (e) {
                    console.log(e)
                }
            })
        });
    }
    var updateCartDesc = function () {
        const languageId = $("#currentLang").val();
        const productUrl = $("#aliasProduct").val();
        //redirect the user to the cart and pass showSuccess=true in the URL
        window.location = "/" + languageId + "/" + productUrl + "/" + productId + "?addSucess=true";
    };
    if (window.location.href.indexOf('addSucess') != -1) {
        $('.add-cart-msg').hide()
            .fadeIn('300').fadeOut(1500);
    }
}

function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}