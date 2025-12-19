document.addEventListener("DOMContentLoaded", function () {

    console.log("Loading map...")

    const mapElement = document.getElementById("map");
    if (!mapElement || typeof L === "undefined") return;

    // -----------------------------
    // Base map setup
    // -----------------------------
    const defaultLat = parseFloat(mapElement.dataset.lat) || 46.55906465244069;
    const defaultLng = parseFloat(mapElement.dataset.lng) || 15.638064980498713;
    const defaultZoom = 15;

    const map = L.map("map").setView([defaultLat, defaultLng], defaultZoom);

    L.tileLayer("https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png", {
        attribution: "© OpenStreetMap contributors"
    }).addTo(map);

    // -----------------------------
    // MODE 1: HUB MAP (multiple hubs)
    // -----------------------------
    if (typeof hubs !== "undefined" && Array.isArray(hubs)) {

        console.log("Loading Multiple Markers...")

        const bounds = L.latLngBounds();

        hubs.forEach(hub => {
            if (!hub.Latitude || !hub.Longitude) return;

            const color = hub.status === "Open" ? "green" : "red";

            const marker = L.circleMarker(
                [hub.Latitude, hub.Longitude],
                {
                    radius: 8,
                    color: color,
                    fillOpacity: 0.85
                }
            ).addTo(map);

            marker.bindPopup(`
                <strong>${hub.Name}</strong><br/>
                Status: ${hub.Status}<br/>
                Capacity: ${hub.Capacity}<br/>
                <a href="/EditVehicleHub?id=${hub.Id}">Edit</a>
            `);

            bounds.extend(marker.getLatLng());
        });

        if (bounds.isValid()) {
            map.fitBounds(bounds, { padding: [40, 40] });
        }

        return; // 🚨 IMPORTANT: do not run Add/Edit logic
    }
    
    console.log("Loading Single Marker...")

    // -----------------------------
    // MODE 2: ADD / EDIT HUB (single marker)
    // -----------------------------
    const latInput = document.getElementById("Latitude");
    const lngInput = document.getElementById("Longitude");

    if (!latInput || !lngInput) return;

    const markerLat = latInput.value ? parseFloat(latInput.value) : defaultLat;
    const markerLng = lngInput.value ? parseFloat(lngInput.value) : defaultLng;

    const marker = L.marker([markerLat, markerLng], { draggable: true }).addTo(map);

    function updateInputs(latlng) {
        latInput.value = latlng.lat;
        lngInput.value = latlng.lng;
    }

    marker.on("dragend", function () {
        updateInputs(marker.getLatLng());
    });

    map.on("click", function (e) {
        marker.setLatLng(e.latlng);
        updateInputs(e.latlng);
    });
});