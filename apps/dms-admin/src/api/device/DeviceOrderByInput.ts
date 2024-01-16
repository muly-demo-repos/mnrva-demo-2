import { SortOrder } from "../../util/SortOrder";

export type DeviceOrderByInput = {
  createdAt?: SortOrder;
  deviceId?: SortOrder;
  deviceName?: SortOrder;
  id?: SortOrder;
  updatedAt?: SortOrder;
};
