import { SortOrder } from "../../util/SortOrder";

export type EventOrderByInput = {
  createdAt?: SortOrder;
  deviceId?: SortOrder;
  eventType?: SortOrder;
  humidity?: SortOrder;
  id?: SortOrder;
  temperature?: SortOrder;
  timestamp?: SortOrder;
  updatedAt?: SortOrder;
};
