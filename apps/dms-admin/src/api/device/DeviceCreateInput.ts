import { EventCreateNestedManyWithoutDevicesInput } from "./EventCreateNestedManyWithoutDevicesInput";

export type DeviceCreateInput = {
  deviceId?: string | null;
  deviceName?: string | null;
  events?: EventCreateNestedManyWithoutDevicesInput;
};
