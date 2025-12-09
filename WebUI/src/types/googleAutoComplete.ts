// Let TS know about google
declare global {
    interface Window {
        google: any
    }
}

import type { DirectiveBinding } from "vue";

function extractStreet(place: any): string {
    let streetNumber = "";
    let route = "";

    place.address_components.forEach((c: any) => {
        if (c.types.includes("street_number")) {
            streetNumber = c.long_name;
        }
        if (c.types.includes("route")) {
            route = c.long_name;
        }
    });

    return [streetNumber, route].filter(Boolean).join(" ");
}

export default {
    mounted(el: HTMLInputElement, binding: DirectiveBinding) {
        const onGoogleReady = setInterval(() => {
            if (window.google?.maps?.places) {
                clearInterval(onGoogleReady);

                const autocomplete = new window.google.maps.places.Autocomplete(el, {
                    types: ["address"],
                    fields: ["address_components", "geometry"], // important: no formatted_address
                });

                autocomplete.addListener("place_changed", () => {
                    const place = autocomplete.getPlace();
                    if (!place.address_components) return;

                    const address = binding.value;

                    // map each component
                    const components: Record<string, string> = {};
                    place.address_components.forEach((c: any) => {
                        components[c.types[0]] = c.long_name;
                    });

                    address.street = extractStreet(place);
                    address.city = components.locality || "";
                    address.state = place.address_components.find((c: any) =>
                      c.types.includes("administrative_area_level_1")
                    )?.short_name || "";
                    address.postalCode = components.postal_code || "";
                    address.country = components.country || "";
                });
            }
        }, 100);
    }
};
