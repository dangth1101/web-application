import { Layout } from "antd";
import Header from "../Header/Header";
import Content from "../Content/Content";
import Footer from "../Footer/Footer";
export default function Root(): JSX.Element {
  return (
    <Layout>
      <Header />
      <Content />
      <Footer />
    </Layout>
  );
}
