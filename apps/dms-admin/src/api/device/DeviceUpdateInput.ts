import { EventUpdateManyWithoutDevicesInput } from "./EventUpdateManyWithoutDevicesInput";

export type DeviceUpdateInput = {
  deviceId?: string | null;
  deviceName?: string | null;
  events?: EventUpdateManyWithoutDevicesInput;
};
