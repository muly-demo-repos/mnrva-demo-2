import * as React from "react";

import {
  Edit,
  SimpleForm,
  EditProps,
  ReferenceInput,
  SelectInput,
  TextInput,
  NumberInput,
  DateTimeInput,
} from "react-admin";

import { DeviceTitle } from "../device/DeviceTitle";

export const EventEdit = (props: EditProps): React.ReactElement => {
  return (
    <Edit {...props}>
      <SimpleForm>
        <ReferenceInput source="device.id" reference="Device" label="device">
          <SelectInput optionText={DeviceTitle} />
        </ReferenceInput>
        <TextInput label="eventType" source="eventType" />
        <NumberInput label="humidity" source="humidity" />
        <NumberInput label="temperature" source="temperature" />
        <DateTimeInput label="timestamp" source="timestamp" />
      </SimpleForm>
    </Edit>
  );
};
