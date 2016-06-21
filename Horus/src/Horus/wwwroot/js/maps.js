// Note: This example requires that you consent to location sharing when
// prompted by your browser. If you see the error "The Geolocation service
// failed.", it means you probably did not give permission for the browser to
// locate you.

var cityParks = {
  park1 : {
    center : {lat: 20.985344 , lng: -86.828616 },
    color: '#FF0000'
  },
  park2 : {
    center: {lat: 20.988688, lng: -86.828485},
    color: '#58FA58'
  },
  park3 : {
    center: {lat: 20.987746 , lng: -86.831468},
    color: '#D0FA58'
  }
};

function initMap() {
  var latLng = {lat:20.987402, lng: -86.828344};
  var map = new google.maps.Map(document.getElementById('map'), {
    center: latLng,
    zoom: 17
  });
  var infoWindow = new google.maps.InfoWindow({map: map});

  var marker = new google.maps.Marker({
    position: latLng,
    draggable: true,
    map: map,
    title: 'Alert Zone' 
  });
  google.maps.event.addListener(marker, "dragend", function() {
    console.log(marker.getPosition().lat());
    console.log(marker.getPosition().lng());
  });
  marker.setMap(map);
  console.log(marker.position);
  console.log("Hi");
  // Try HTML5 geolocation.
  if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition(function(position) { 
      var pos = {
        lat: 20.987402,
        lng: -86.828344
      };

      infoWindow.setPosition(pos);
      infoWindow.setContent('I found you! ;)');
      map.setCenter(pos);
    }, function() {
      handleLocationError(true, infoWindow, map.getCenter());
    });
  } else {
    // Browser doesn't support Geolocation
    handleLocationError(false, infoWindow, map.getCenter());
  }

  for(var parks in cityParks){
      console.log(parks)
      var parkCircle = new google.maps.Circle({
        strokeColor: cityParks[parks].color,
        strokeOpacity: 0.8,
        strokeWeight: 2,
        fillColor:cityParks[parks].color,
        fillOpacity: 0.35,
        map: map,
        center: cityParks[parks].center,
        radius: 100
        });
  }
}

function handleLocationError(browserHasGeolocation, infoWindow, pos) {
  infoWindow.setPosition(pos);
  infoWindow.setContent(browserHasGeolocation ?
                        'Error: The Geolocation service failed.' :
                        'Error: Your browser doesn\'t support geolocation.');
}
