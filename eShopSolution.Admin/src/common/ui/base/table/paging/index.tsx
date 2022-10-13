import React, { FC, HTMLAttributes, useEffect, useState } from "react";
import style from "./paging.module.scss";
import PagingItem from "./PagingItem";

interface Props extends HTMLAttributes<HTMLElement> {
  currentPage: number;
  totalPage: number;
  onGetData: (page: number, ...arg: Array<any>) => void;
  extra?: Array<any>;
}

const CTPaging: FC<Props> = (props: Props) => {
  const { currentPage, totalPage, onGetData, extra = [] } = props;

  const [hasNext, setHasNext] = useState<boolean>(false);
  const [hasPrev, setHasPrev] = useState<boolean>(false);
  const [pageItems, setPageItems] = useState<Array<number>>([]);

  const generatePageItems = (): void => {
    let start = currentPage - 2;
    let end = currentPage + 2;

    if (start <= 0) {
      end += Math.abs(start) + 1;
      start = 1;
    }

    if (end > totalPage) end = totalPage;

    const items = [];
    for (let i = start; i <= end; i++) items.push(i);
    setPageItems(items);
  };

  useEffect(() => {
    setHasNext(currentPage < totalPage);
    setHasPrev(currentPage > 1);
    generatePageItems();
    // eslint-disable-next-line
  }, [currentPage, totalPage]);

  useEffect(() => {}, [extra]);

  return (
    <div className={`${style.paging} ${props.className}`} style={props.style}>
      <div className={style.content}>
        <PagingItem
          prev
          active={hasPrev}
          onClick={() => onGetData(currentPage - 1, ...extra)}
        />
        {pageItems.map((page, index) => (
          <PagingItem
            key={index}
            active={page === currentPage}
            onClick={() => onGetData(page, ...extra)}
          >
            {page}
          </PagingItem>
        ))}
        <PagingItem
          next
          active={hasNext}
          onClick={() => onGetData(currentPage + 1, ...extra)}
        />
      </div>
    </div>
  );
};

export default CTPaging;
