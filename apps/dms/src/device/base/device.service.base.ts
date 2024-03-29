/*
------------------------------------------------------------------------------ 
This code was generated by Amplication. 
 
Changes to this file will be lost if the code is regenerated. 

There are other ways to to customize your code, see this doc to learn more
https://docs.amplication.com/how-to/custom-code

------------------------------------------------------------------------------
  */
import { PrismaService } from "../../prisma/prisma.service";

import {
  Prisma,
  Device, // @ts-ignore
  Event,
} from "@prisma/client";

export class DeviceServiceBase {
  constructor(protected readonly prisma: PrismaService) {}

  async count<T extends Prisma.DeviceCountArgs>(
    args: Prisma.SelectSubset<T, Prisma.DeviceCountArgs>
  ): Promise<number> {
    return this.prisma.device.count(args);
  }

  async devices<T extends Prisma.DeviceFindManyArgs>(
    args: Prisma.SelectSubset<T, Prisma.DeviceFindManyArgs>
  ): Promise<Device[]> {
    return this.prisma.device.findMany(args);
  }
  async device<T extends Prisma.DeviceFindUniqueArgs>(
    args: Prisma.SelectSubset<T, Prisma.DeviceFindUniqueArgs>
  ): Promise<Device | null> {
    return this.prisma.device.findUnique(args);
  }
  async createDevice<T extends Prisma.DeviceCreateArgs>(
    args: Prisma.SelectSubset<T, Prisma.DeviceCreateArgs>
  ): Promise<Device> {
    return this.prisma.device.create<T>(args);
  }
  async updateDevice<T extends Prisma.DeviceUpdateArgs>(
    args: Prisma.SelectSubset<T, Prisma.DeviceUpdateArgs>
  ): Promise<Device> {
    return this.prisma.device.update<T>(args);
  }
  async deleteDevice<T extends Prisma.DeviceDeleteArgs>(
    args: Prisma.SelectSubset<T, Prisma.DeviceDeleteArgs>
  ): Promise<Device> {
    return this.prisma.device.delete(args);
  }

  async findEvents(
    parentId: string,
    args: Prisma.EventFindManyArgs
  ): Promise<Event[]> {
    return this.prisma.device
      .findUniqueOrThrow({
        where: { id: parentId },
      })
      .events(args);
  }
}
