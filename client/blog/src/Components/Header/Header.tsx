import { Button, Flex, Layout, Menu } from "antd";
import { ItemType, MenuItemType } from "antd/es/menu/interface";
import "./Header.scss";
const items: ItemType<MenuItemType>[] = [
  {
    key: "blog1",
    label: "BLOG",
  },
  {
    key: "blog2",
    label: "BLOG",
  },
  {
    key: "blog3",
    label: "BLOG",
  },
];

export default function Header(): JSX.Element {
  return (
    <Layout >
      <Flex
        className="flex-header"
        gap={"small"}
        justify="space-between"
        align="center"
      >
        <div>Logo</div>
        <Menu
          className="menu-header"
          mode="horizontal"
          defaultSelectedKeys={["blog1"]}
          items={items}
        />
        <Button type="text">Login</Button>
      </Flex>
    </Layout>
  );
}
