/**
 * Theme: Metrica - Responsive Bootstrap 4 Admin Dashboard
 * Author: Mannatthemes
 * Dashboard Js
 */
function GetChart() {

    //colunm-1

    var optionsProductBrand = {
        chart: {
            height: 295,
            type: 'bar',
            toolbar: {
                show: true
            }
        },
        plotOptions: {
            bar: {
                horizontal: false,
                endingShape: 'rounded',
                columnWidth: '30%',
                dataLabels: {
                    position: 'top', // top, center, bottom
                },
            },
        },
        dataLabels: {
            enabled: true,
            formatter: function (val) {
                return val + "";
            },
            offsetY: -10,
            style: {
                fontSize: '12px',
                colors: ["#ff4560"]
            }
        },
        stroke: {
            show: true,
            width: 2,
            colors: ['transparent']
        },
        //colors: ["#000", '#fff','#eee'],
        series: [{
            name: 'Nông nghiệp',
            data: [NongNghiep]
        }, {
            name: 'Chăn nuôi',
            data: [ChanNuoi]

        }, {
            name: 'Thủy hải sản',
            data: [ThuyHaiSan]
        },],
        xaxis: {
            categories: ['NGÀNH'],
            axisBorder: {
                show: true,
                color: '#bec7e0',
            },
            axisTicks: {
                show: true,
                color: '#bec7e0',
            },
        },
        legend: {
            offsetY: 6,
        },
        yaxis: {
            title: {
                text: 'Số lượng',
            },
        },
        fill: {
            opacity: 1

        },
        // legend: {
        //     floating: true
        // },
        grid: {
            row: {
                colors: ['transparent', 'transparent'], // takes an array which will be repeated on columns
                opacity: 0.2,
            },
            strokeDashArray: 4,
        },
        tooltip: {
            y: {
                formatter: function (val) {
                    return "" + val + " doanh nghiệp"
                }
            }
        }
    }

    var chartProductBrand = new ApexCharts(
        document.querySelector("#ana_dash_ProductBrand"),
        optionsProductBrand
    );

    var optionsProduct = {
        chart: {
            height: 295,
            type: 'bar',
            toolbar: {
                show: true
            }
        },
        plotOptions: {
            bar: {
                horizontal: false,
                endingShape: 'rounded',
                columnWidth: '30%',
                dataLabels: {
                    position: 'top', // top, center, bottom
                },
            },
        },
        dataLabels: {
            enabled: true,
            formatter: function (val) {
                return val + "";
            },
            offsetY: -10,
            style: {
                fontSize: '12px',
                colors: ["#ff4560"]
            }
        },
        stroke: {
            show: true,
            width: 2,
            colors: ['transparent']
        },
        //colors: ["rgba(42, 118, 244, .18)", '#2a76f4'],
        series: [{
            name: 'Nông nghiệp',
            data: [NongNghiepSP]
        }, {
            name: 'Chăn nuôi',
            data: [ChanNuoiSP]

        }, {
            name: 'Thủy hải sản',
            data: [ThuyHaiSanSP]
        },],
        xaxis: {
            categories: ['SẢN PHẨM THEO NGÀNH'],
            axisBorder: {
                show: true,
                color: '#000',
            },
            axisTicks: {
                show: true,
                color: '#000',
            },
        },
        legend: {
            offsetY: 6,
        },
        yaxis: {
            title: {
                text: 'Số lượng',
            },
        },
        fill: {
            opacity: 1

        },
        // legend: {
        //     floating: true
        // },
        grid: {
            row: {
                colors: ['transparent', 'transparent'], // takes an array which will be repeated on columns
                opacity: 0.2,
            },
            strokeDashArray: 4,
        },
        tooltip: {
            y: {
                formatter: function (val) {
                    return "" + val + " sản phẩm"
                }
            }
        }
    }

    var chartProduct = new ApexCharts(
        document.querySelector("#ana_dash_Product"),
        optionsProduct
    );

    var optionsProductPackage = {
        chart: {
            height: 295,
            type: 'bar',
            toolbar: {
                show: true
            }
        },
        plotOptions: {
            bar: {
                horizontal: false,
                endingShape: 'rounded',
                columnWidth: '30%',
                dataLabels: {
                    position: 'top', // top, center, bottom
                },
            },
        },
        dataLabels: {
            enabled: true,
            formatter: function (val) {
                return val + "";
            },
            offsetY: -10,
            style: {
                fontSize: '12px',
                colors: ["#ff4560"]
            }
        },
        stroke: {
            show: true,
            width: 2,
            colors: ['transparent']
        },
        //colors: ["rgba(42, 118, 244, .18)", '#2a76f4'],
        series: [{
            name: 'Đang sản xuất',
            data: [DangSanXuat]
        }, {
            name: 'Đang thu hoạch',
            data: [DangThuHoach]

        }, {
            name: 'Thu hoạch xong',
            data: [ThuHoachXong]
        }, {
            name: 'Hủy',
            data: [Huy]
        },],
        xaxis: {
            categories: ['TRẠNG THÁI LÔ SẢN XUẤT'],
            axisBorder: {
                show: true,
                color: '#000',
            },
            axisTicks: {
                show: true,
                color: '#000',
            },
        },
        legend: {
            offsetY: 6,
        },
        yaxis: {
            title: {
                text: 'Số lượng',
            },
        },
        fill: {
            opacity: 1

        },
        // legend: {
        //     floating: true
        // },
        grid: {
            row: {
                colors: ['transparent', 'transparent'], // takes an array which will be repeated on columns
                opacity: 0.2,
            },
            strokeDashArray: 4,
        },
        tooltip: {
            y: {
                formatter: function (val) {
                    return "" + val + " lô"
                }
            }
        }
    }

    var chartProductPackage = new ApexCharts(
        document.querySelector("#ana_dash_ProductPackage"),
        optionsProductPackage
    );
    var chartProductPackage1 = new ApexCharts(
        document.querySelector("#ana_dash_ProductPackage1"),
        optionsProductPackage
    );
    if ($('#ana_dash_Product').length > 0)
        chartProduct.render();
    if ($('#ana_dash_ProductBrand').length > 0)
        chartProductBrand.render();
    if ($('#ana_dash_ProductPackage').length > 0)
        chartProductPackage.render();
    if ($('#ana_dash_ProductPackage1').length > 0)
        chartProductPackage1.render();
}
function GetChartProductPackageOrder() {
    var optionsProductPackageOrder = {
        chart: {
            height: 295,
            type: 'bar',
            toolbar: {
                show: true
            }
        },

        plotOptions: {
            bar: {
                horizontal: false,
                endingShape: 'rounded',
                columnWidth: '30%',
                dataLabels: {
                    position: 'top', // top, center, bottom
                },
            },
        },
        dataLabels: {
            enabled: true,
            formatter: function (val) {
                return val + "";
            },
            offsetY: -10,
            style: {
                fontSize: '12px',
                colors: ["#ff4560"]
            }
        },
        stroke: {
            show: true,
            width: 2,
            colors: ['transparent']
        },
        //colors: ["rgba(42, 118, 244, .18)", '#2a76f4'],
        series: [{
            name: 'Lệnh chưa duyệt',
            data: [Lenhchuaduyet]
        }, {
            name: 'Lệnh đã duyệt',
            data: [Lenhdaduyet]

        },],
        xaxis: {
            categories: ['LỆNH SẢN XUẤT'],
            axisBorder: {
                show: true,
                color: '#bec7e0',
            },
            axisTicks: {
                show: true,
                color: '#bec7e0',
            },
        },
        legend: {
            offsetY: 6,
        },
        yaxis: {
            title: {
                text: 'Số lượng',
            },
        },
        fill: {
            opacity: 1

        },
        // legend: {
        //     floating: true
        // },
        grid: {
            row: {
                colors: ['transparent', 'transparent'], // takes an array which will be repeated on columns
                opacity: 0.2,
            },
            strokeDashArray: 4,
        },
        tooltip: {
            y: {
                formatter: function (val) {
                    return "" + val + " Lệnh sản xuất"
                }
            }
        }
    }

    var chartProductPackageOrder = new ApexCharts(
        document.querySelector("#ana_dash_ProductPackageOrder"),
        optionsProductPackageOrder
    );
    if ($('#ana_dash_ProductPackageOrder').length > 0)
        chartProductPackageOrder.render();

    var chartProductPackageOrder1 = new ApexCharts(
        document.querySelector("#ana_dash_ProductPackageOrder1"),
        optionsProductPackageOrder
    );
    if ($('#ana_dash_ProductPackageOrder1').length > 0)
        chartProductPackageOrder1.render();
}
var d = new Date();
var YearNow = d.getFullYear();
function GetChartProductBrand() {

    var options = {
        chart: {
            height: 380,
            type: 'area',
            stacked: false,
            toolbar: {
                show: true,
                autoSelected: 'zoom',
                tools: {
                    download: true,
                },

            },
            zoom: {
                type: 'x',
                enabled: true,
                autoScaleYaxis: true
            },
            //dropShadow: {
            //    enabled: false,
            //    top: 12,
            //    left: 0,
            //    bottom: 0,
            //    right: 0,
            //    blur: 2,
            //    color: '#45404a2e',
            //    opacity: 0.35
            //},
        },

        colors: ['#ff4560', '#00e396'],
        dataLabels: {
            enabled: false
        },
        stroke: {
            curve: 'smooth',
            width: [4, 4],
            dashArray: [0, 3]
        },
        grid: {
            borderColor: "#45404a2e",
            padding: {
                left: 0,
                right: 0
            },
            strokeDashArray: 4,
        },
        markers: {
            size: 0,
            hover: {
                size: 0
            }
        },
        series: [{
            name: 'Năm ' + YearNow + ' <span>(Tổng ' + TotalNow + ' doanh nghiệp)</span>',
            data: listNow
        }, {
            name: 'Năm ' + (YearNow - 1) + ' <span>(Tổng ' + TotalLast + ' doanh nghiệp)</span>',
            data: listLast
        }
        ],

        xaxis: {
            type: 'month',
            categories: ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6', 'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'],
            axisBorder: {
                show: true,
                color: '#45404a2e',
            },
            axisTicks: {
                show: true,
                color: '#45404a2e',
            },
        },

        fill: {
            //type: 'gradient',
            //gradient: {
            //    gradientToColors: ['#F55555', '#B5AC49', '#6094ea']
            //},
        },
        tooltip: {
            x: {
                format: 'dd/MM/yy HH:mm'
            },
            y: {
                formatter: function (val) {
                    return "" + val + " doanh nghiệp"
                }
            }
        },
        legend: {
            position: 'bottom',
            itemMargin: {
                vertical: 20
            },
            //horizontalAlign: 'right'
        },
    }

    //var options = {
    //    series: [
    //        {
    //            name: "High - 2013",
    //            data: [28, 29, 33, 36, 32, 32, 33]
    //        },
    //        {
    //            name: "Low - 2013",
    //            data: [12, 11, 14, 18, 17, 13, 13]
    //        }
    //    ],
    //    chart: {
    //        height: 350,
    //        type: 'line',
    //        dropShadow: {
    //            enabled: true,
    //            color: '#000',
    //            top: 18,
    //            left: 7,
    //            blur: 10,
    //            opacity: 0.2
    //        },
    //        toolbar: {
    //            show: false
    //        }
    //    },
    //    colors: ['#77B6EA', '#545454'],
    //    dataLabels: {
    //        enabled: true,
    //    },
    //    stroke: {
    //        curve: 'smooth'
    //    },
    //    title: {
    //        text: 'Average High & Low Temperature',
    //        align: 'left'
    //    },
    //    grid: {
    //        borderColor: '#e7e7e7',
    //        row: {
    //            colors: ['#f3f3f3', 'transparent'], // takes an array which will be repeated on columns
    //            opacity: 0.5
    //        },
    //    },
    //    markers: {
    //        size: 1
    //    },
    //    xaxis: {
    //        categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul'],
    //        title: {
    //            text: 'Month'
    //        }
    //    },
    //    yaxis: {
    //        title: {
    //            text: 'Temperature'
    //        },
    //        min: 5,
    //        max: 40
    //    },
    //    legend: {
    //        position: 'top',
    //        horizontalAlign: 'right',
    //        floating: true,
    //        offsetY: -25,
    //        offsetX: -5
    //    }
    //};
    var chart = new ApexCharts(
        document.querySelector("#ana_dash_ProductBrandReport"),
        options
    );

    if ($('#ana_dash_ProductBrandReport').length > 0)
        chart.render();
}

function GetChartWarehouseExport() {
    var optionsWarehouseExport = {
        chart: {
            height: 295,
            type: 'bar',
            toolbar: {
                show: true
            }
        },

        plotOptions: {
            bar: {
                horizontal: false,
                endingShape: 'rounded',
                columnWidth: '30%',
                dataLabels: {
                    position: 'top', // top, center, bottom
                },
            },
        },
        dataLabels: {
            enabled: true,
            formatter: function (val) {
                return val + "";
            },
            offsetY: -10,
            style: {
                fontSize: '12px',
                colors: ["#ff4560"]
            }
        },
        stroke: {
            show: true,
            width: 2,
            colors: ['transparent']
        },
        //colors: ["rgba(42, 118, 244, .18)", '#2a76f4'],
        series: [{
            name: 'Quý 1',
            data: [precious1]
        }, {
            name: 'Quý 2',
            data: [precious2]

        }, {
            name: 'Quý 3',
            data: [precious3]

        }, {
            name: 'Quý 4',
            data: [precious4]

        },],
        xaxis: {
            categories: ['THỐNG KÊ PHIẾU XUẤT KHO '],
            axisBorder: {
                show: true,
                color: '#bec7e0',
            },
            axisTicks: {
                show: true,
                color: '#bec7e0',
            },
        },
        legend: {
            offsetY: 6,
        },
        yaxis: {
            title: {
                text: 'Số lượng',
            },
        },
        fill: {
            opacity: 1

        },
        // legend: {
        //     floating: true
        // },
        grid: {
            row: {
                colors: ['transparent', 'transparent'], // takes an array which will be repeated on columns
                opacity: 0.2,
            },
            strokeDashArray: 4,
        },
        tooltip: {
            y: {
                formatter: function (val) {
                    return "" + val + " Phiếu Xuất Kho"
                }
            }
        }
    }

    var GetChartWarehouseExport = new ApexCharts(
        document.querySelector("#ana_dash_WarehouseExport"),
        optionsWarehouseExport
    );
    if ($('#ana_dash_WarehouseExport').length > 0)
        GetChartWarehouseExport.render();
}

function GetChartWarehouseProductExport() {
    var optionsWarehouseProductExport = {
        chart: {
            height: 295,
            type: 'bar',
            toolbar: {
                show: true
            }
        },

        plotOptions: {
            bar: {
                horizontal: false,
                endingShape: 'rounded',
                columnWidth: '30%',
                dataLabels: {
                    position: 'top', // top, center, bottom
                },
            },
        },
        dataLabels: {
            enabled: true,
            formatter: function (val) {
                return val + "";
            },
            offsetY: -10,
            style: {
                fontSize: '12px',
                colors: ["#ff4560"]
            }
        },
        stroke: {
            show: true,
            width: 2,
            colors: ['transparent']
        },
        //colors: ["rgba(42, 118, 244, .18)", '#2a76f4'],
        series: [{
            name: 'Quý 1',
            data: [Product_precious1]
        }, {
            name: 'Quý 2',
            data: [Product_precious2]

        }, {
            name: 'Quý 3',
            data: [Product_precious3]

        }, {
            name: 'Quý 4',
            data: [Product_precious4]

        },],
        xaxis: {
            categories: ['THỐNG KÊ PHIẾU XUẤT KHO SẢN PHẨM'],
            axisBorder: {
                show: true,
                color: '#bec7e0',
            },
            axisTicks: {
                show: true,
                color: '#bec7e0',
            },
        },
        legend: {
            offsetY: 6,
        },
        yaxis: {
            title: {
                text: 'Số lượng',
            },
        },
        fill: {
            opacity: 1

        },
        // legend: {
        //     floating: true
        // },
        grid: {
            row: {
                colors: ['transparent', 'transparent'], // takes an array which will be repeated on columns
                opacity: 0.2,
            },
            strokeDashArray: 4,
        },
        tooltip: {
            y: {
                formatter: function (val) {
                    return "" + val + " Phiếu xuất"
                }
            }
        }
    }

    var GetChartWarehouseProductExport = new ApexCharts(
        document.querySelector("#ana_dash_WarehouseProductExport"),
        optionsWarehouseProductExport
    );
    if ($('#ana_dash_WarehouseProductExport').length > 0)
        GetChartWarehouseProductExport.render();
}


function GetChartZone() {
    var optionsZone = {
        chart: {
            height: 295,
            type: 'bar',
            toolbar: {
                show: true
            }
        },

        plotOptions: {
            bar: {
                horizontal: false,
                endingShape: 'rounded',
                columnWidth: '30%',
                dataLabels: {
                    position: 'top', // top, center, bottom
                },
            },
        },
        dataLabels: {
            enabled: true,
            formatter: function (val) {
                return val + "";
            },
            offsetY: -10,
            style: {
                fontSize: '12px',
                colors: ["#ff4560"]
            }
        },
        stroke: {
            show: true,
            width: 2,
            colors: ['transparent']
        },
        //colors: ["rgba(42, 118, 244, .18)", '#2a76f4'],
        series: [{
            name: 'Vùng',
            data: [Zone]
        }, {
            name: 'Khu',
            data: [Area]

        }, {
            name: 'Thửa',
            data: [Farm]

        }],
        xaxis: {
            categories: ['THỐNG KÊ KHU VỰC SẢN XUẤT'],
            axisBorder: {
                show: true,
                color: '#bec7e0',
            },
            axisTicks: {
                show: true,
                color: '#bec7e0',
            },
        },
        legend: {
            offsetY: 6,
        },
        yaxis: {
            title: {
                text: 'Số lượng',
            },
        },
        fill: {
            opacity: 1

        },
        // legend: {
        //     floating: true
        // },
        grid: {
            row: {
                colors: ['transparent', 'transparent'], // takes an array which will be repeated on columns
                opacity: 0.2,
            },
            strokeDashArray: 4,
        },
        tooltip: {
            y: {
                formatter: function (val) {
                    return "" + val + " "
                }
            }
        }
    }

    var GetChartZone = new ApexCharts(
        document.querySelector("#ana_dash_Zone"),
        optionsZone
    );
    if ($('#ana_dash_Zone').length > 0)
        GetChartZone.render();
}

function GetChartQRCode() {
    var optionsZone = {
        chart: {
            height: 295,
            type: 'bar',
            toolbar: {
                show: true
            }
        },

        plotOptions: {
            bar: {
                horizontal: false,
                endingShape: 'rounded',
                columnWidth: '30%',
                dataLabels: {
                    position: 'top', // top, center, bottom
                },
            },
        },
        dataLabels: {
            enabled: true,
            formatter: function (val) {
                return val + "";
            },
            offsetY: -10,
            style: {
                fontSize: '12px',
                colors: ["#ff4560"]
            }
        },
        stroke: {
            show: true,
            width: 2,
            colors: ['transparent']
        },
        //colors: ["rgba(42, 118, 244, .18)", '#2a76f4'],
        series: [{
            name: 'Tem đã sinh',
            data: [QRCode]
        }, {
            name: 'Tem kích hoạt',
            data: [QRCodeActive]

        }, {
            name: 'Tem chưa kích hoạt',
            data: [QRCodeNotActive]

        }],
        xaxis: {
            categories: ['THỐNG KÊ LÔ MÃ '],
            axisBorder: {
                show: true,
                color: '#bec7e0',
            },
            axisTicks: {
                show: true,
                color: '#bec7e0',
            },
        },
        legend: {
            offsetY: 6,
        },
        yaxis: {
            title: {
                text: 'Số lượng',
            },
        },
        fill: {
            opacity: 1

        },
        // legend: {
        //     floating: true
        // },
        grid: {
            row: {
                colors: ['transparent', 'transparent'], // takes an array which will be repeated on columns
                opacity: 0.2,
            },
            strokeDashArray: 4,
        },
        tooltip: {
            y: {
                formatter: function (val) {
                    return "" + val + " "
                }
            }
        }
    }

    var GetChartZone = new ApexCharts(
        document.querySelector("#ana_dash_QRCode"),
        optionsZone
    );
    if ($('#ana_dash_QRCode').length > 0)
        GetChartZone.render();
}
// traffice chart

//var options = {
//    chart: {
//        height: 250,
//        type: 'area',
//        stacked: true,
//        toolbar: {
//            show: false,
//            autoSelected: 'zoom'
//        },
//    },
//    colors: ['#2a77f4', '#1ccab8'],
//    dataLabels: {
//        enabled: false
//    },
//    stroke: {
//        curve: 'smooth',
//        width: [2, 2],
//        dashArray: [0, 2]
//    },
//    grid: {
//        borderColor: "#45404a2e",
//        padding: {
//            left: 0,
//            right: 0
//        },
//        strokeDashArray: 4,
//    },
//    markers: {
//        size: 0,
//        hover: {
//            size: 0
//        }
//    },
//    series: [{
//        name: 'Organic Search',
//        data: [0, 60, 20, 90, 45, 110, 55, 130, 44, 110, 75, 200]
//    }, {
//        name: 'Social Media',
//        data: [0, 45, 10, 75, 35, 94, 40, 115, 30, 105, 65, 190]
//    }],



//    xaxis: {
//        type: 'time',
//        categories: ["00:00", "01:30", "02:30", "03:30", "04:30", "05:30", "06:30", "07:30", "08:30", "09:30", "10:30", "11:30"],
//        axisBorder: {
//            show: true,
//            color: '#45404a2e',
//        },
//        axisTicks: {
//            show: true,
//            color: '#45404a2e',
//        },
//    },

//    tooltip: {
//        x: {
//            format: 'HH:mm'
//        },
//    },
//    legend: {
//        position: 'top',
//        horizontalAlign: 'right'
//    },
//}

//var chart = new ApexCharts(
//    document.querySelector("#liveVisits"),
//    options
//);

//chart.render();


////Device-widget


//var options = {
//    chart: {
//        height: 200,
//        type: 'donut',
//    },
//    plotOptions: {
//        pie: {
//            donut: {
//                size: '80%'
//            }
//        }
//    },
//    dataLabels: {
//        enabled: false,
//    },

//    stroke: {
//        show: true,
//        width: 2,
//        colors: ['transparent']
//    },

//    series: [10, 65, 25,],
//    legend: {
//        show: false,
//        position: 'bottom',
//        horizontalAlign: 'center',
//        verticalAlign: 'middle',
//        floating: false,
//        fontSize: '14px',
//        offsetX: 0,
//        offsetY: 5
//    },
//    labels: ["Tablet", "Desktop", "Mobile"],
//    colors: ["#fda354", "#506ee4", "#41cbd8"],

//    responsive: [{
//        breakpoint: 600,
//        options: {
//            plotOptions: {
//                donut: {
//                    customScale: 0.2
//                }
//            },
//            chart: {
//                height: 240
//            },
//            legend: {
//                show: false
//            },
//        }
//    }],

//    tooltip: {
//        y: {
//            formatter: function (val) {
//                return val + " %"
//            }
//        }
//    }

//}

//var chart = new ApexCharts(
//    document.querySelector("#ana_device"),
//    options
//);

//chart.render();


//// map

//$('#usa').vectorMap({
//    map: 'us_aea_en',
//    backgroundColor: 'transparent',
//    borderColor: '#818181',
//    regionStyle: {
//        initial: {
//            fill: '#506ee424',
//        }
//    },
//    series: {
//        regions: [{
//            values: {
//                "US-VA": '#506ee452',
//                "US-PA": '#506ee452',
//                "US-TN": '#506ee452',
//                "US-WY": '#506ee452',
//                "US-WA": '#506ee452',
//                "US-TX": '#506ee452',
//            },
//            attribute: 'fill',
//        }]
//    },
//});