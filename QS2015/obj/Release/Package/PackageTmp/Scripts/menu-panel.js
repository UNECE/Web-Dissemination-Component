jQuery(document).ready(function ($) {

    // push down/hide menu
    $(".btn-slide").click(function(){
        $(".menu-settings-panel").slideToggle("slow");
        $(this).toggleClass("active"); return false;
    });


    // country is added/removed in charts
	$('.country-selection-element').on('click', function (event) {
	    var chkboxId = '#chk' + event.target.id;
	    $(chkboxId).click();
    });

	$('.country-selection-checkbox').on('click', function (event) {
	    getchartData();
    });


    // **************************************************************************************************************

	function getchartData() {
	    var thisCountriesList = '';

	    $('.country-checkbox').each(function () {
	        if (this.checked) {
	            if (thisCountriesList == '') {
	                thisCountriesList = this.id;
	            } else {
	                thisCountriesList = thisCountriesList + ',' + this.id;
	            }
	        }
	    });

	    if (thisCountriesList != '') {
	        // get fresh chart data
	        $.getJSON("/PXWeb/" + mylang + "/Charts/GetChartByCodes", { indicatorCode: thisIndicatorCode, countriesList: thisCountriesList }, function (data) {
	            multiseriesChart = new FusionCharts(data.fusioncharts);
	            multiseriesChart.setChartAttribute("showValues", "0");
	            multiseriesChart.render();
	        });
	    }
    }



	$('.previous-chart-single-btn').on('click', function (event) {
	    var thisLength = indCodes.length;
	    if (thisLength > 1) {
	        var thisIndCode = indCodes[thisLength - 2];

	        $.getJSON("/PXWeb/" + mylang + "/Charts/GetChartByIndicator", { indicatorCode: thisIndCode }, function (data) {
	            if (data != null) {
	                updateChart(data);
	                indCodes.pop();
	            }
	        });

	    }
	});



    // go to next chart
	$('.next-chart-single-btn').on('click', function (event) {
	    $.getJSON("/PXWeb/" + mylang + "/Charts/GetNextChart", { indicatorCode: thisIndicatorCode }, function (data) {
	        if (data != null) {
	            updateChart(data);
	            indCodes.push(thisIndicatorCode);
	        }
	    });
	});

	function updateChart(data) {
	    multiseriesChart = new FusionCharts(data.fusioncharts);
	    multiseriesChart.setChartAttribute("showValues", "0");
	    multiseriesChart.render();

	    thisIndicatorCode = data.indicator;
	    thisCountry = data.countries[0];

	    // change weblink in the address bar
	    window.history.pushState("object or string", "Title", "/PXWeb/" + mylang + "/Charts?IndicatorCode=" + thisIndicatorCode + "&CountryCode=" + thisCountry);

	    // mark the default country
	    $('.country-checkbox').each(function () {
	        if (this.value == thisCountry) {
	            this.checked = true;
	        } else {
	            this.checked = false;
	        }


	    });


	    // make visible/invisible row divs	            
	    $('.country-row').each(function () {
	        var rowid = this.id.substring(0, this.id.length - 1);

	        if (data.countries.indexOf(rowid) == -1) {
	            this.style.display = "none";
	        }
	        else {
	            this.style.display = "table-row";
	        }
	    });
	}

    // **************************************************************************************************************

    // go to next map
	$('.next-map-btn').on('click', function (event) {
	    $.getJSON("/PXWeb/" + mylang + "/DataMap/GetRandomMap", null, function (data) {
	        $.each(data, function (i, item) {
	            updateMap(item);
	            indCodes.push(item.IndicatorCode);
	        });
	    });

	});

    // single map only
	$('.next-map-single-btn').on('click', function (event) {
	    $.getJSON("/PXWeb/" + mylang + "/DataMap/GetNextMap", { indCode: indCodes[indCodes.length - 1] }, function (data) {
	        $.each(data, function (i, item) {
	            updateMap(item);
	            indCodes.push(item.IndicatorCode);

	            // change address bar in the map window
	            window.history.pushState("object or string", "Title", "/PXWeb/" + mylang + "/DataMap?IndicatorCode=" + item.IndicatorCode);
	        });
	    });

	});


	$('.previous-map-btn').on('click', function (event) {

	    var thisLength = indCodes.length;
	    if (thisLength>1) {
	        var thisIndCode = indCodes[thisLength - 2];
	        
	        $.getJSON("/PXWeb/" + mylang + "/DataMap/GetMapByIndicatorCode",
                {
                    indicatorCode: thisIndCode
                },
                function (data) {
	                $.each(data, function (i, item) {
	                    updateMap(item);
	                    indCodes.pop();
	            });
	        });
	    }
	});


	$('.previous-map-single-btn').on('click', function (event) {
	    var thisLength = indCodes.length;
	    if (thisLength > 1) {
	        var thisIndCode = indCodes[thisLength - 2];

	        $.getJSON("/PXWeb/" + mylang + "/DataMap/GetMapByIndicatorCode",
                {
                    indicatorCode: thisIndCode
                },
                function (data) {
                    $.each(data, function (i, item) {
                        updateMap(item);
                        indCodes.pop();

                        // change address bar in the map window
                        window.history.pushState("object or string", "Title", "/PXWeb/" + mylang + "/DataMap?IndicatorCode=" + thisIndCode);
                    });
                });
	    }
	});


	function updateMap(item) {
	    $("div#map").fadeOut(0);
	    
	    // find map title div
	    var div = $('#map-title');
	    var footnoteDiv = $('div#footnote');

	    // init variables
	    var mapTitle = item.Title;
	    var footnote = item.Footnote;
	    var period = item.Period;        

	    //div.text(mapTitle + ', year ' + period);
	    div.text(mapTitle + ' (' + period + ')');
	    footnoteDiv.html(footnote);

	    measurementUnit = item.MeasurementUnit;

	    mygrades = item.GradeValues.split(",").map(Number);
	    mycolors = item.ColorValues.split(",");

	    hasc_codes = item.CountriesList.split(",");
	    myvalues = item.ValuesList.split(",").map(Number);

	    // update data 
	    map.removeLayer(geojson);

	    geojson = L.geoJson(statesData, {
	        style: style,
	        onEachFeature: onEachFeature
	    }).addTo(map);

	    // set info box to empty text
	    $('.info').html('-');

	    // update legend
	    $('.legend').remove();
	    var legend = L.control({ position: 'bottomright' });

	    legend.onAdd = function (map) {

	        var div = L.DomUtil.create('div', 'info legend'),
                grades = mygrades,
                labels = [],
                from, to;

            for (var i = 0; i < grades.length; i++) {
	            from = grades[i];
	            to = grades[i + 1];

	            labels.push(
                    '<i style="background:' + getColor(from + 0.000001) + '"></i> ' +
                    numberWithSpace(from) + ((to != null) ? '&nbsp;&ndash;&nbsp;' + numberWithSpace(to) : '+')
                    );
            }

	        div.innerHTML = labels.join('<br>');

	        return div;
        };

	    legend.addTo(map);

	    $("div#map").fadeIn(1500);

	}

});


