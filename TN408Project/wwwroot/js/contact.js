
    $(document).ready(function()
    {
        // load district
        $("#city").on('change', function(){
            var cityId = $(this).val();
            $.ajax({
                method: "GET",
                url: "contact_ward",
                data: { cityId: cityId },
                dataType: "html",
                success: function (data){
                    $('#district').html(data);
                }
            });
        });

        // load ward
        $("#district").on('change', function(){
            var districtId = $(this).val();
            $.ajax({
                method: "GET",
                url: "contact_ward",
                data: { districtId: districtId },
                dataType: "html",
                success: function (data){
                    $('#ward').html(data);
                }
            });
        });
    });