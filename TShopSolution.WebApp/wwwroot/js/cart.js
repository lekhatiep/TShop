var CartController = function () {
    this.initialize = function () {
        UpdateData();
        DelItemData();
        LoadData();
    }
    function UpdateData() {
        $('body').on("click", '.btn_plus', function (e) {
            e.preventDefault();

            const id = $(this).data('id');
            const languageId = $("#currentLang").val();
            const quantity = parseInt($(".quantity_" + id + "").val()) + 1;

            UpdateItem(languageId, id, quantity);
        });

        $('body').on("click", '.btn_minus', function (e) {
            e.preventDefault();

            const id = $(this).data('id');
            const languageId = $("#currentLang").val();
            const quantity = parseInt($(".quantity_" + id + "").val()) - 1;

            UpdateItem(languageId, id, quantity);
        });
    }
    function DelItemData() {
        $('body').on("click", '.btn_remove', function (e) {
            e.preventDefault();
            const languageId = $("#currentLang").val();
            const id = $(this).data('id');

            $.ajax({
                method: "POST",
                url: "/" + languageId + "/Cart/DeleteItemCart",
                data: {
                    id: id,
                },
                success: function () {
                    LoadData();
                },
                error: function (e) {
                }
            })
        });
    }

    function LoadData() {
        const languageId = $("#currentLang").val();
        const baseAddress = $("#baseAdd").val();
        $.ajax({
            method: "GET",
            url: "/" + languageId + "/Cart/GetListCart",
            success: function (res) {
                if (res.length === 0) {
                    $(".table_item_cart").hide();
                }

                var html = '';
                var total = 0;
                var numberItem = 0;

                $.each(res, function (i, item) {
                    var ammount = numberItem += item.quantity;
                    amount = item.price * item.quantity;
                    html += "<tr>"
                        + "<td><img width=\"60\" src=\"" + baseAddress + item.image + "\" alt=\"\" /></td>"
                        + "<td>" + item.description + "</td>"
                        + "<td>"
                        + "<div class=\"input-append\"><input value=" + item.quantity + " class=\"span1 quantity_" + item.productId + "\" style=\"max-width:34px\" placeholder=\"1\" id=\"product_id" + item.productId + "\" size=\"16\" type=\"text\"><button class=\"btn btn_minus\" data-id=" + item.productId + " type=\"button\"><i class=\"icon-minus\"></i></button><button class=\"btn btn_plus\" data-id=" + item.productId + " type=\"button\"><i class=\"icon-plus\"></i><\/button><button class=\"btn btn-danger btn_remove\" data-id=" + item.productId + " type=\"button\"><i class=\"icon-remove icon-white\"></i><\/button>"
                        + "</div>"
                        + "</td>"
                        + "<td>" + numberWithCommas(item.price) + "</td>"
                        + "<td>" + numberWithCommas(amount) + "</td>"
                        + "</tr>";
                    total += amount;
                });

                $("#lbl_total").text(numberWithCommas(total));
                $("#tbl_cart_body").html(html);
                $("#number_item_header").text(numberItem);
                $(".item_header").text(numberItem);
            },
            error: function (e) {
                console.log(e)
            }
        })
    }

    function numberWithCommas(x) {
        return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    }

    function UpdateItem(languageId, id, quantity) {
        $.ajax({
            method: "POST",
            url: "/" + languageId + "/Cart/UpdateCart",
            data: {
                id: id,
                languageId: languageId,
                quantity: quantity
            },
            success: function () {
                LoadData();
            },
            error: function (e) {
            }
        })
    }
}