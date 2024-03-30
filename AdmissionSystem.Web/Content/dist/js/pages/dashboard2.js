$(function () {

  'use strict'

  /* ChartJS
   * -------
   * Here we will create a few charts using ChartJS
   */

  //-----------------------
  //- MONTHLY SALES CHART -
  //-----------------------

  // Get context with jQuery - using jQuery's .get() method.
  var salesChartCanvas = $('#salesChart').get(0).getContext('2d')

  var salesChartData = {
    labels  : ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
    datasets: [
      {
        label               : 'Digital Goods',
        backgroundColor     : 'rgba(60,141,188,0.9)',
        borderColor         : 'rgba(60,141,188,0.8)',
        pointRadius          : false,
        pointColor          : '#3b8bba',
        pointStrokeColor    : 'rgba(60,141,188,1)',
        pointHighlightFill  : '#fff',
        pointHighlightStroke: 'rgba(60,141,188,1)',
        data                : [28, 48, 40, 19, 86, 27, 90]
      },
      {
        label               : 'Electronics',
        backgroundColor     : 'rgba(210, 214, 222, 1)',
        borderColor         : 'rgba(210, 214, 222, 1)',
        pointRadius         : false,
        pointColor          : 'rgba(210, 214, 222, 1)',
        pointStrokeColor    : '#c1c7d1',
        pointHighlightFill  : '#fff',
        pointHighlightStroke: 'rgba(220,220,220,1)',
        data                : [65, 59, 80, 81, 56, 55, 40]
      },
    ]
  }

  var salesChartOptions = {
    maintainAspectRatio : false,
    responsive : true,
    legend: {
      display: false
    },
    scales: {
      xAxes: [{
        gridLines : {
          display : false,
        }
      }],
      yAxes: [{
        gridLines : {
          display : false,
        }
      }]
    }
  }

  // This will get the first returned node in the jQuery collection.
  var salesChart = new Chart(salesChartCanvas, { 
      type: 'line', 
      data: salesChartData, 
      options: salesChartOptions
    }
  )

  //---------------------------
  //- END MONTHLY SALES CHART -
  //---------------------------

  //-------------
  //- PIE CHART -
  //-------------
  // Get context with jQuery - using jQuery's .get() method.
    var pieChartCanvas = $('#pieChart').get(0).getContext('2d')
    var pieData        = {
      labels: [
          'Chrome', 
          'IE',
          'FireFox', 
          'Safari', 
          'Opera', 
          'Navigator', 
      ],
      datasets: [
        {
          data: [700,500,400,600,300,100],
          backgroundColor : ['#f56954', '#00a65a', '#f39c12', '#00c0ef', '#3c8dbc', '#d2d6de'],
        }
      ]
    }
    var pieOptions     = {
      legend: {
        display: false
      }
    }
    //Create pie or douhnut chart
    // You can switch between pie and douhnut using the method below.
    var pieChart = new Chart(pieChartCanvas, {
      type: 'doughnut',
      data: pieData,
      options: pieOptions      
    })

  //-----------------
  //- END PIE CHART -
  //-----------------

  /* jVector Maps
   * ------------
   * Create a world map with markers
   */
  //$('#world-map-markers').mapael({
  //    map: {
  //      name : "usa_states",
  //      zoom: {
  //        enabled: true,
  //        maxLevel: 10
  //      },
  //    },
  //  }
  //);

  // $('#world-map-markers').vectormap({
  //   map              : 'world_en',
  //   normalizefunction: 'polynomial',
  //   hoveropacity     : 0.7,
  //   hovercolor       : false,
  //   backgroundcolor  : 'transparent',
  //   regionstyle      : {
  //     initial      : {
  //       fill            : 'rgba(210, 214, 222, 1)',
  //       'fill-opacity'  : 1,
  //       stroke          : 'none',
  //       'stroke-width'  : 0,
  //       'stroke-opacity': 1
  //     },
  //     hover        : {
  //       'fill-opacity': 0.7,
  //       cursor        : 'pointer'
  //     },
  //     selected     : {
  //       fill: 'yellow'
  //     },
  //     selectedhover: {}
  //   },
  //   markerstyle      : {
  //     initial: {
  //       fill  : '#00a65a',
  //       stroke: '#111'
  //     }
  //   },
  //   markers          : [
  //     {
  //       latlng: [41.90, 12.45],
  //       name  : 'vatican city'
  //     },
  //     {
  //       latlng: [43.73, 7.41],
  //       name  : 'monaco'
  //     },
  //     {
  //       latlng: [-0.52, 166.93],
  //       name  : 'nauru'
  //     },
  //     {
  //       latlng: [-8.51, 179.21],
  //       name  : 'tuvalu'
  //     },
  //     {
  //       latlng: [43.93, 12.46],
  //       name  : 'san marino'
  //     },
  //     {
  //       latlng: [47.14, 9.52],
  //       name  : 'liechtenstein'
  //     },
  //     {
  //       latlng: [7.11, 171.06],
  //       name  : 'marshall islands'
  //     },
  //     {
  //       latlng: [17.3, -62.73],
  //       name  : 'saint kitts and nevis'
  //     },
  //     {
  //       latlng: [3.2, 73.22],
  //       name  : 'maldives'
  //     },
  //     {
  //       latlng: [35.88, 14.5],
  //       name  : 'malta'
  //     },
  //     {
  //       latlng: [12.05, -61.75],
  //       name  : 'grenada'
  //     },
  //     {
  //       latlng: [13.16, -61.23],
  //       name  : 'saint vincent and the grenadines'
  //     },
  //     {
  //       latlng: [13.16, -59.55],
  //       name  : 'barbados'
  //     },
  //     {
  //       latlng: [17.11, -61.85],
  //       name  : 'antigua and barbuda'
  //     },
  //     {
  //       latlng: [-4.61, 55.45],
  //       name  : 'seychelles'
  //     },
  //     {
  //       latlng: [7.35, 134.46],
  //       name  : 'palau'
  //     },
  //     {
  //       latlng: [42.5, 1.51],
  //       name  : 'andorra'
  //     },
  //     {
  //       latlng: [14.01, -60.98],
  //       name  : 'saint lucia'
  //     },
  //     {
  //       latlng: [6.91, 158.18],
  //       name  : 'federated states of micronesia'
  //     },
  //     {
  //       latlng: [1.3, 103.8],
  //       name  : 'singapore'
  //     },
  //     {
  //       latlng: [1.46, 173.03],
  //       name  : 'kiribati'
  //     },
  //     {
  //       latlng: [-21.13, -175.2],
  //       name  : 'tonga'
  //     },
  //     {
  //       latlng: [15.3, -61.38],
  //       name  : 'dominica'
  //     },
  //     {
  //       latlng: [-20.2, 57.5],
  //       name  : 'mauritius'
  //     },
  //     {
  //       latlng: [26.02, 50.55],
  //       name  : 'bahrain'
  //     },
  //     {
  //       latlng: [0.33, 6.73],
  //       name  : 'são tomé and príncipe'
  //     }
  //   ]
  // })

})
